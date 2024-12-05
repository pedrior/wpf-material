using System.Windows.Input;

namespace WPF.Material.Components;

/// <summary>
/// Represents a selectable item in a <see cref="NavigationRail"/>.
/// </summary>
[TemplatePart(Name = PartRipple, Type = typeof(Container))]
public class NavigationRailItem : NavigationItem
{
    private const string PartRipple = "PART_Ripple";

    private Ripple? ripple;

    static NavigationRailItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationRailItem),
            new FrameworkPropertyMetadata(typeof(NavigationRailItem)));
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ripple = GetTemplateChild(PartRipple) as Ripple ??
                 throw new NullReferenceException($"Missing required template part '{PartRipple}'.");
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        ripple?.Start(hold: true);
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonUp(e);

        ripple?.Release();
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        base.OnMouseLeave(e);

        ripple?.Release();
    }
}