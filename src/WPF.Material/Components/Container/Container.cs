namespace WPF.Material.Components;

/// <summary>
/// Represents a container that can be used to host other controls. A container has its appearance defined by a
/// <see cref="Surface"/> that expresses shape, elevation and state.
/// </summary>
[TemplatePart(Name = PartRipple, Type = typeof(Ripple))]
public class Container : ContentControl
{
    private const string PartRipple = "Ripple";

    private RippleController? rippleController;

    static Container()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Container),
            new FrameworkPropertyMetadata(typeof(Container)));
    }

    /// <summary>
    /// Gets the <see cref="RippleController"/> that can be used to control the ripple effect of the container.
    /// </summary>
    public RippleController RippleController =>
        rippleController ?? throw new InvalidOperationException(
            "The template part 'Ripple' must be present in the control template.");

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild(PartRipple) is Ripple ripple)
        {
            rippleController = new RippleController(ripple);
        }
    }
}