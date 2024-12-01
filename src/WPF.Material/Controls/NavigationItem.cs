namespace WPF.Material.Controls;

/// <summary>
/// Represents a base selectable control for navigation items. 
/// </summary>
public abstract class NavigationItem : ToggleButtonBase
{
    static NavigationItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationItem),
            new FrameworkPropertyMetadata(typeof(NavigationItem)));
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
}