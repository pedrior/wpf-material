namespace WPF.Material.Components;

/// <summary>
/// Represents a control that presents a collection of items for navigation rail actions. This control is typically
/// used within a <see cref="NavigationRail"/> to display top or bottom actions.
/// </summary>
public class NavigationRailActions : ItemsControl
{
    static NavigationRailActions()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationRailActions),
            new FrameworkPropertyMetadata(typeof(NavigationRailActions)));
    }
}