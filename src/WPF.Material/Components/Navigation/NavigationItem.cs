namespace WPF.Material.Components;

/// <summary>
/// Represents a base selectable control for navigation items. 
/// </summary>
public abstract class NavigationItem : System.Windows.Controls.Primitives.ToggleButton
{
    /// <summary>
    /// Identifies the <see cref="RequestSubDestinationsSheet"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RequestSubDestinationsSheetProperty = DependencyProperty.Register(
        nameof(RequestSubDestinationsSheet),
        typeof(bool),
        typeof(NavigationItem),
        new PropertyMetadata(false));
    
    static NavigationItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationItem),
            new FrameworkPropertyMetadata(typeof(NavigationItem)));
        
        IsThreeStateProperty.OverrideMetadata(
            typeof(NavigationItem),
            new FrameworkPropertyMetadata(false, null, CoerceIsThreeState));
    }
    
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="NavigationItem"/> requests a <see cref="Sheet"/>
    /// component for displaying sub-destinations related to this item.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool RequestSubDestinationsSheet
    {
        get => (bool)GetValue(RequestSubDestinationsSheetProperty);
        set => SetValue(RequestSubDestinationsSheetProperty, value);
    }

    protected override void OnToggle()
    {
        if (Parent is NavigationRail rail)
        {
            rail.Toggle(this);
        }
        else
        {
            base.OnToggle();
        }
    }
    
    private static object CoerceIsThreeState(DependencyObject element, object value) => false;
}