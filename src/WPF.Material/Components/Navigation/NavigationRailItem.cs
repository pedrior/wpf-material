using System.Windows.Input;

namespace WPF.Material.Components;

/// <summary>
/// Represents an item in a <see cref="NavigationRail"/> component.
/// </summary>
[TemplatePart(Name = PartRipple, Type = typeof(Container))]
public class NavigationRailItem : System.Windows.Controls.Primitives.ToggleButton
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

        ripple?.Start(e.GetPosition(ripple));
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