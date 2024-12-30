using System.Diagnostics;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WPF.Material.Components;

/// <summary>
/// Represents a control that toggles between two states.
/// </summary>
[TemplatePart(Name = PartTrack, Type = typeof(Track))]
public class Switch : System.Windows.Controls.CheckBox
{
    private const double CheckedPosition = 1.0;
    private const double MiddlePosition = 0.5;
    private const double UncheckedPosition = 0.0;

    private const double AcceptableClickOffset = 0.15;

    private const string PartTrack = "PART_Track";

    private readonly DoubleAnimation checkedAnimation = new()
    {
        To = CheckedPosition,
        FillBehavior = FillBehavior.Stop,
        Duration = AnimationDurations.Short200,
        EasingFunction = AnimationEasings.Standard
    };

    private readonly DoubleAnimation uncheckedAnimation = new()
    {
        To = UncheckedPosition,
        FillBehavior = FillBehavior.Stop,
        Duration = AnimationDurations.Short200,
        EasingFunction = AnimationEasings.Standard
    };

    private Track? track;

    static Switch()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Switch),
            new FrameworkPropertyMetadata(typeof(Switch)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Switch"/> class.
    /// </summary>
    public Switch()
    {
        Unloaded += OnUnloaded;

        checkedAnimation.Completed += OnCheckedAnimationCompleted;
        uncheckedAnimation.Completed += OnUncheckedAnimationCompleted;
    }

    // <inheritdoc/>
    [Obsolete("This property is not intended to be used.")]
    public new object? Content
    {
        get => base.Content;
        set => base.Content = value;
    }
    
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (track is not null)
        {
            track.Thumb.DragDelta -= OnThumbDragDelta;
            track.Thumb.DragCompleted -= OnThumbDragCompleted;
            track.Thumb.PreviewMouseLeftButtonDown -= OnThumbPreviewMouseLeftButtonDown;
            track.Thumb.PreviewMouseLeftButtonUp -= OnThumbPreviewMouseLeftButtonUp;
        }

        track = GetTemplateChild(PartTrack) as Track
                ?? throw new NullReferenceException($"Missing template part '{PartTrack}'.");

        Debug.Assert(track.Maximum is CheckedPosition, "The maximum value of the track should be 1.0");
        Debug.Assert(track.Minimum is UncheckedPosition, "The minimum value of the track should be 0.0");

        if (IsChecked is true)
        {
            RepositionThumbBasedOnState(true, isAnimate: false);
        }

        track.Thumb.DragDelta += OnThumbDragDelta;
        track.Thumb.DragCompleted += OnThumbDragCompleted;
        track.Thumb.PreviewMouseLeftButtonDown += OnThumbPreviewMouseLeftButtonDown;
        track.Thumb.PreviewMouseLeftButtonUp += OnThumbPreviewMouseLeftButtonUp;
    }

    protected override void OnChecked(RoutedEventArgs e)
    {
        base.OnChecked(e);

        RepositionThumbBasedOnState(true);
    }

    protected override void OnUnchecked(RoutedEventArgs e)
    {
        base.OnUnchecked(e);

        RepositionThumbBasedOnState(false);
    }

    private void OnThumbDragDelta(object sender, DragDeltaEventArgs e)
    {
        var horizontalOffset = EvaluateChangedHorizontalOffset(e.HorizontalChange);
        var position = Math.Clamp(track!.Value + horizontalOffset, UncheckedPosition, CheckedPosition);

        track.Value = position;
    }

    private void OnThumbDragCompleted(object sender, DragCompletedEventArgs e)
    {
        // Checks if the user's drag was so minimal that it should be considered a click.
        var horizontalOffset = EvaluateChangedHorizontalOffset(e.HorizontalChange);
        if (Math.Abs(horizontalOffset) < AcceptableClickOffset)
        {
            Toggle();

            return;
        }

        var position = track!.Value;
        if (position is CheckedPosition)
        {
            SetCurrentValue(IsCheckedProperty, true);
        }
        else if (position is UncheckedPosition)
        {
            SetCurrentValue(IsCheckedProperty, false);
        }
        else
        {
            var shouldChange = position > MiddlePosition && IsChecked is not true ||
                               position < MiddlePosition && IsChecked is true;
            if (shouldChange)
            {
                Toggle();
            }
            else
            {
                // We aren't changing the state, but at this point, the thumb is not in the desired position,
                // so we need to reposition it based on the current unchanged state.
                RepositionThumbBasedOnState(IsChecked is true);
            }
        }
    }

    private void OnThumbPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => IsPressed = true;

    private void OnThumbPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) => IsPressed = false;

    private double EvaluateChangedHorizontalOffset(double horizontalChange) =>
        track!.ValueFromDistance(horizontalChange, vertical: 0.0);

    private void RepositionThumbBasedOnState(bool isChecked, bool isAnimate = true)
    {
        if (isAnimate)
        {
            track?.BeginAnimation(Track.ValueProperty, isChecked
                ? checkedAnimation
                : uncheckedAnimation);
        }
        else
        {
            track?.SetValue(Track.ValueProperty, isChecked
                ? CheckedPosition
                : UncheckedPosition);
        }
    }

    private void Toggle() => SetCurrentValue(IsCheckedProperty, IsChecked is null or false);

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= OnUnloaded;

        checkedAnimation.Completed -= OnCheckedAnimationCompleted;
        uncheckedAnimation.Completed -= OnUncheckedAnimationCompleted;
    }

    private void OnCheckedAnimationCompleted(object? sender, EventArgs e) => track!.Value = CheckedPosition;

    private void OnUncheckedAnimationCompleted(object? sender, EventArgs e) => track!.Value = UncheckedPosition;
}