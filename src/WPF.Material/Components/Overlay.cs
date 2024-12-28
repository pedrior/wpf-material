namespace WPF.Material.Components;

/// <summary>
/// Displays a tinted overlay over a region bounded by a <see cref="Geometry"/>.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public class Overlay : FrameworkElement
{
    /// <summary>
    /// Identifies the <see cref="Brush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
        nameof(Brush),
        typeof(Brush),
        typeof(Overlay),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="OutlineGeometry"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OutlineGeometryProperty = DependencyProperty.Register(
        nameof(OutlineGeometry),
        typeof(Geometry),
        typeof(Overlay),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Gets or sets a <see cref="System.Windows.Media.Brush"/> that describes the overlay's color. The default value is
    /// <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Brush)]
    public Brush? Brush
    {
        get => (Brush?)GetValue(BrushProperty);
        set => SetValue(BrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Geometry"/> that defines the overlay's region. The default value is
    /// <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    [TypeConverter(typeof(GeometryConverter))]
    public Geometry? OutlineGeometry
    {
        get => (Geometry?)GetValue(OutlineGeometryProperty);
        set => SetValue(OutlineGeometryProperty, value);
    }

    protected override void OnRender(DrawingContext context)
    {
        base.OnRender(context);

        var tint = Brush;
        if (tint is not null)
        {
            context.DrawGeometry(
                tint, 
                pen: null,
                OutlineGeometry ?? new RectangleGeometry(new Rect(RenderSize)));
        }
    }
}