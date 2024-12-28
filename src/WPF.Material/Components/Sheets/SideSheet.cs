using System.Windows.Media.Animation;

namespace WPF.Material.Components;

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

    private AnimationTimeline? enterAnimation;
    private AnimationTimeline? exitAnimation;

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
    /// default for undocked side sheets. The default value is <see langword="true"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool ShowDivider
    {
        get => (bool)GetValue(ShowDividerProperty);
        set => SetValue(ShowDividerProperty, value);
    }

    protected override double TargetSize => ActualWidth;

    protected override AnimationTimeline EnterAnimation => GetEnterAnimation();

    protected override AnimationTimeline ExitAnimation => GetExitAnimation();

    internal override DependencyProperty TargetSizeProperty => WidthProperty;

    private AnimationTimeline GetEnterAnimation()
    {
        if (enterAnimation is not null)
        {
            return enterAnimation;
        }

        enterAnimation = CreateBaseAnimation(TargetSize, freeze: false);
        enterAnimation.Completed += OnEnterAnimationCompleted;

        enterAnimation.Freeze();
        return enterAnimation;
    }

    private AnimationTimeline GetExitAnimation()
    {
        if (exitAnimation is not null)
        {
            return exitAnimation;
        }

        exitAnimation = CreateBaseAnimation(0, freeze: false);
        exitAnimation.Completed += OnExitAnimationCompleted;

        exitAnimation.Freeze();
        return exitAnimation;
    }

    private void OnEnterAnimationCompleted(object? sender, EventArgs e) => OnOpened();

    private void OnExitAnimationCompleted(object? sender, EventArgs e) => OnClosed();
}