namespace WPF.Material.Components;

/// <summary>
/// Represents a toggle button that is part of a <see cref="SegmentedButtons"/>.
/// </summary>
public class SegmentedButtonItem : System.Windows.Controls.Primitives.ToggleButton
{
    /// <summary>
    /// Identifies the <see cref="ShowIcon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowIconProperty = DependencyProperty.Register(
        nameof(ShowIcon),
        typeof(bool),
        typeof(SegmentedButtonItem),
        new PropertyMetadata(true));
    
    static SegmentedButtonItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SegmentedButtonItem),
            new FrameworkPropertyMetadata(typeof(SegmentedButtonItem)));
        
        IsThreeStateProperty.OverrideMetadata(
            typeof(SegmentedButtonItem),
            new FrameworkPropertyMetadata(false, null, CoerceIsThreeState));
    }
    
    /// <summary>
    /// Gets or sets a value indicating whether the icon should be shown. When the button is checked, the
    /// <see cref="Icon.IconOnSelectingProperty"/> will be used, otherwise the <see cref="Icon.IconProperty"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public bool ShowIcon
    {
        get => (bool)GetValue(ShowIconProperty);
        set => SetValue(ShowIconProperty, value);
    }
    
    protected override void OnToggle()
    {
        if (Parent is SegmentedButtons group)
        {
            group.ToggleItem(this);
        }
        else
        {
            base.OnToggle();
        }
    }
    
    private static object CoerceIsThreeState(DependencyObject element, object value) => false;
}