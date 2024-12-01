using System.Windows.Input;

namespace WPF.Material.Controls;

/// <summary>
/// Represents a selectable item in a <see cref="NavigationRail"/>.
/// </summary>
[TemplatePart(Name = PartContainer, Type = typeof(Container))]
public class NavigationRailItem : NavigationItem
{
    private const string PartContainer = "Container";

   private Container container = null!;
    
    static NavigationRailItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationRailItem),
            new FrameworkPropertyMetadata(typeof(NavigationRailItem)));
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        container = GetTemplateChild(PartContainer) as Container
                    ?? throw new InvalidOperationException($"Missing required template part: {PartContainer}");
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        
        container.RippleController.StartAndKeep();
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonUp(e);

        container.RippleController.Release();
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        base.OnMouseLeave(e);
        
        container.RippleController.Release();
    }
}