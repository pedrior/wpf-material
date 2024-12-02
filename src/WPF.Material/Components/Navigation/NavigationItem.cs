namespace WPF.Material.Components;

/// <summary>
/// Represents a base selectable control for navigation items. 
/// </summary>
public abstract class NavigationItem : System.Windows.Controls.Primitives.ToggleButton
{
    static NavigationItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationItem),
            new FrameworkPropertyMetadata(typeof(NavigationItem)));
        
        IsThreeStateProperty.OverrideMetadata(
            typeof(NavigationItem),
            new FrameworkPropertyMetadata(false, null, CoerceIsThreeState));
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