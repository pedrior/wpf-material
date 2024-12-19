namespace WPF.Material.Components;

/// <summary>
/// Displays a tinted overlay over a region bounded by a <see cref="Geometry"/>.
/// </summary>
public class Overlay : FrameworkElement
{
    /// <summary>
    /// Identifies the <see cref="Tint"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TintProperty = DependencyProperty.Register(
        nameof(Tint),
        typeof(Brush),
        typeof(Overlay),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="DefiningGeometry"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DefiningGeometryProperty = DependencyProperty.Register(
        nameof(DefiningGeometry),
        typeof(Geometry),
        typeof(Overlay),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Gets or sets a <see cref="Brush"/> that describes the overlay's color. The default value is
    /// <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public Brush? Tint
    {
        get => (Brush?)GetValue(TintProperty);
        set => SetValue(TintProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Geometry"/> that defines the overlay's region. The default value is
    /// <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    [TypeConverter(typeof(GeometryConverter))]
    public Geometry? DefiningGeometry
    {
        get => (Geometry?)GetValue(DefiningGeometryProperty);
        set => SetValue(DefiningGeometryProperty, value);
    }

    protected override void OnRender(DrawingContext context)
    {
        base.OnRender(context);

        var tint = Tint;
        if (tint is not null)
        {
            context.DrawGeometry(
                tint, 
                pen: null,
                DefiningGeometry ?? new RectangleGeometry(new Rect(RenderSize)));
        }
    }
}