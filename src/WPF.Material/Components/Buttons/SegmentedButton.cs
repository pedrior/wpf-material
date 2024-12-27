namespace WPF.Material.Components;

/// <summary>
/// Represents a control that behaves like a <see cref="Button"/>, but is intended to be part of a
/// <see cref="SegmentedButtonGroup"/> as a segment that can be toggled on or off.
/// </summary>
public class SegmentedButton : System.Windows.Controls.Primitives.ToggleButton
{
    /// <summary>
    /// Identifies the <see cref="IsIconVisible"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsIconVisibleProperty = DependencyProperty.Register(
        nameof(IsIconVisible),
        typeof(bool),
        typeof(SegmentedButton),
        new PropertyMetadata(true));
    
    static SegmentedButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SegmentedButton),
            new FrameworkPropertyMetadata(typeof(SegmentedButton)));
    }
    
    /// <summary>
    /// Gets or sets a value indicating whether the icon is visible. The default value is <see langword="true"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public bool IsIconVisible
    {
        get => (bool)GetValue(IsIconVisibleProperty);
        set => SetValue(IsIconVisibleProperty, value);
    }
    
    protected override void OnToggle()
    {
        if (Parent is SegmentedButtonGroup group)
        {
            group.Select(this);
        }
        else
        {
            base.OnToggle();
        }
    }
}