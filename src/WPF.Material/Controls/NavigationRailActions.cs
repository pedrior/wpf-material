namespace WPF.Material.Controls;

/// <summary>
/// Represents a control that contains a collection navigation rail actions in a <see cref="NavigationRail"/> control.
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