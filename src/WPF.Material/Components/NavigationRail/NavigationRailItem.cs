using System.Windows.Input;

namespace WPF.Material.Components;

/// <summary>
/// Represents a selectable item in a <see cref="NavigationRail"/>.
/// </summary>
[TemplatePart(Name = PartRipple, Type = typeof(Container))]
public class NavigationRailItem : System.Windows.Controls.Primitives.ToggleButton
{
    /// <summary>
    /// Identifies the <see cref="RequestSubDestinationsSheet"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RequestSubDestinationsSheetProperty = DependencyProperty.Register(
        nameof(RequestSubDestinationsSheet),
        typeof(bool),
        typeof(NavigationRailItem),
        new PropertyMetadata(false));
    
    private const string PartRipple = "PART_Ripple";

    private Ripple? ripple;

    static NavigationRailItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationRailItem),
            new FrameworkPropertyMetadata(typeof(NavigationRailItem)));
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="NavigationRailItem"/> requests a <see cref="Sheet"/>
    /// component for displaying sub-destinations related to this item.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool RequestSubDestinationsSheet
    {
        get => (bool)GetValue(RequestSubDestinationsSheetProperty);
        set => SetValue(RequestSubDestinationsSheetProperty, value);
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