using System.Windows.Media.Animation;
using WPF.Material.Environment;

namespace WPF.Material.Controls;

/// <summary>
/// Represents a <see cref="Sheet"/> that is anchored horizontally to its parent.
/// </summary>
public class SideSheet : Sheet
{
    /// <summary>
    /// Identifies the <see cref="ShowDivider"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowDividerProperty = DependencyProperty.Register(
        nameof(ShowDivider),
        typeof(bool),
        typeof(SideSheet),
        new PropertyMetadata(true));

    private DoubleAnimationUsingKeyFrames? openSheetAnimation;
    private DoubleAnimationUsingKeyFrames? closeSheetAnimation;

    static SideSheet()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SideSheet),
            new FrameworkPropertyMetadata(typeof(SideSheet)));
    }

    /// <summary>
    /// Gets or sets a value that indicates whether this <see cref="SideSheet"/> should show a vertical divider.
    /// When <see langword="true"/>, the divider appears to the left of the content when the side sheet is
    /// right-aligned within the parent's layout slot, and to the right when left-aligned. The divider is hidden by
    /// default for undocked side sheets.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public bool ShowDivider
    {
        get => (bool)GetValue(ShowDividerProperty);
        set => SetValue(ShowDividerProperty, value);
    }

    protected override double TargetSize => ActualWidth;

    protected override AnimationTimeline OpenAnimation => GetOpenAnimation();

    protected override AnimationTimeline CloseAnimation => GetCloseAnimation();

    internal override DependencyProperty TargetSizeProperty => WidthProperty;

    private DoubleAnimationUsingKeyFrames GetOpenAnimation()
    {
        if (openSheetAnimation is not null)
        {
            return openSheetAnimation;
        }

        openSheetAnimation = CreateBaseAnimation(TargetSize, freeze: false);
        openSheetAnimation.Completed += OnOpenAnimationCompleted;

        openSheetAnimation.Freeze();
        return openSheetAnimation;
    }

    private DoubleAnimationUsingKeyFrames GetCloseAnimation()
    {
        if (closeSheetAnimation is not null)
        {
            return closeSheetAnimation;
        }

        closeSheetAnimation = CreateBaseAnimation(0, freeze: false);
        closeSheetAnimation.Completed += OnCloseAnimationCompleted;

        closeSheetAnimation.Freeze();
        return closeSheetAnimation;
    }

    private void OnOpenAnimationCompleted(object? sender, EventArgs e) => OnOpened();

    private void OnCloseAnimationCompleted(object? sender, EventArgs e) => OnClosed();
}