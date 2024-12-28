namespace WPF.Material.Components;

/// <summary>
/// Renders a surface with a customizable shape, background, and border.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public class Surface : FrameworkElement
{
    /// <summary>
    /// Identifies the <see cref="Background"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(
        nameof(Background),
        typeof(Brush),
        typeof(Surface),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="BorderBrush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register(
        nameof(BorderBrush),
        typeof(Brush),
        typeof(Surface),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="BorderThickness"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register(
        nameof(BorderThickness),
        typeof(Thickness),
        typeof(Surface),
        new FrameworkPropertyMetadata(new Thickness(), FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="ShapeFamily"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeFamilyProperty = DependencyProperty.Register(
        nameof(ShapeFamily),
        typeof(ShapeFamily),
        typeof(Surface),
        new FrameworkPropertyMetadata(ShapeFamily.Rounded, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="ShapeStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeStyleProperty = DependencyProperty.Register(
        nameof(ShapeStyle),
        typeof(ShapeStyle),
        typeof(Surface),
        new FrameworkPropertyMetadata(ShapeStyle.None, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="ShapeCorner"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeCornerProperty = DependencyProperty.Register(
        nameof(ShapeCorner),
        typeof(ShapeCorner),
        typeof(Surface),
        new FrameworkPropertyMetadata(ShapeCorner.All, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="ShapeRadius"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeRadiusProperty = DependencyProperty.Register(
        nameof(ShapeRadius),
        typeof(CornerRadius?),
        typeof(Surface),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="UseStyleOnRadiusOverride"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UseStyleOnRadiusOverrideProperty = DependencyProperty.Register(
        nameof(UseStyleOnRadiusOverride),
        typeof(bool),
        typeof(Surface),
        new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="UseCornersOnRadiusOverride"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UseCornersOnRadiusOverrideProperty = DependencyProperty.Register(
        nameof(UseCornersOnRadiusOverride),
        typeof(ShapeCorner),
        typeof(Surface),
        new FrameworkPropertyMetadata(ShapeCorner.All, FrameworkPropertyMetadataOptions.AffectsRender));

    private static readonly DependencyPropertyKey RenderedGeometryPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(RenderedGeometry),
        typeof(Geometry),
        typeof(Surface),
        new FrameworkPropertyMetadata(Geometry.Empty));

    /// <summary>
    /// Identifies the <see cref="RenderedGeometry"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RenderedGeometryProperty = 
        RenderedGeometryPropertyKey.DependencyProperty;

    public event EventHandler? Rendered;

    /// <summary>
    /// Gets or sets a <see cref="Brush"/> that describes the background of the surface. The default value is
    /// <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Brush)]
    public Brush? Background
    {
        get => (Brush?)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets a <see cref="Brush"/> that describes the border of the surface. The default value is
    /// <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Brush)]
    public Brush? BorderBrush
    {
        get => (Brush?)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the border thickness of the surface. The default value is a <see cref="Thickness"/> with all values
    /// set to 0.0.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public Thickness BorderThickness
    {
        get => (Thickness)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }

    /// <summary>
    /// Gets or sets the shape family of the surface. The default value is <see cref="ShapeFamily.Rounded"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public ShapeFamily ShapeFamily
    {
        get => (ShapeFamily)GetValue(ShapeFamilyProperty);
        set => SetValue(ShapeFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the shape style of the surface, which applies a predefined corner radius to the surface.
    /// The default value is <see cref="ShapeStyle.None"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public ShapeStyle ShapeStyle
    {
        get => (ShapeStyle)GetValue(ShapeStyleProperty);
        set => SetValue(ShapeStyleProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that specifies which corners of the surface should have a radius applied. The default value is
    /// <see cref="ShapeCorner.All"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public ShapeCorner ShapeCorner
    {
        get => (ShapeCorner)GetValue(ShapeCornerProperty);
        set => SetValue(ShapeCornerProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom corner radius for the surface. If set when <see cref="UseCornersOnRadiusOverride"/> is
    /// <see cref="ShapeCorner.All"/>, the <see cref="ShapeStyle"/> will not be applied; otherwise, the surface will
    /// have a combination of the custom corner radius and the corner radius from the applied style. See
    /// <see cref="UseStyleOnRadiusOverride"/> and <see cref="UseCornersOnRadiusOverride"/> for more information.
    /// The default value is <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public CornerRadius? ShapeRadius
    {
        get => (CornerRadius?)GetValue(ShapeRadiusProperty);
        set => SetValue(ShapeRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="ShapeStyle"/> should be used when a custom corner
    /// radius is set. If <see langword="true"/>, the applied style radius will be used in combination with the custom
    /// radius when <see cref="UseCornersOnRadiusOverride"/> is not <see cref="ShapeCorner.All"/>. The default value is
    /// <see langword="true"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool UseStyleOnRadiusOverride
    {
        get => (bool)GetValue(UseStyleOnRadiusOverrideProperty);
        set => SetValue(UseStyleOnRadiusOverrideProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating which corners of the surface should have a radius applied when a custom
    /// corner radius is set. If <see cref="ShapeCorner.All"/>, the custom corner radius will be applied to all corners;
    /// if <see cref="ShapeCorner.None"/>, the custom corner radius will not be applied. The default value is
    /// <see cref="ShapeCorner.All"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ShapeCorner UseCornersOnRadiusOverride
    {
        get => (ShapeCorner)GetValue(UseCornersOnRadiusOverrideProperty);
        set => SetValue(UseCornersOnRadiusOverrideProperty, value);
    }

    /// <summary>
    /// Gets a <see cref="Geometry"/> that represents the rendered surface.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Geometry RenderedGeometry
    {
        get => (Geometry)GetValue(RenderedGeometryProperty);
        private set => SetValue(RenderedGeometryPropertyKey, value);
    }

    protected override void OnRender(DrawingContext context)
    {
        base.OnRender(context);

        var width = RenderSize.Width;
        var height = RenderSize.Height;

        var borderBrush = BorderBrush;
        var thickness = borderBrush is null
            ? 0.0
            : Math.Abs(BorderThickness.Left);

        width = Math.Abs(width - thickness);
        height = Math.Abs(height - thickness);

        if (width is 0.0 || height is 0.0)
        {
            // Nothing to render
            return;
        }

        var isStroked = thickness >= 1.0;

        var bounds = new Rect(
            thickness * 0.5,
            thickness * 0.5,
            width,
            height);

        var radius = GetActualCornerRadius(bounds.Width, bounds.Height);
        var geometry = CreateGeometry(ShapeFamily, bounds, radius, isStroked);

        var pen = isStroked
            ? new Pen(BorderBrush, thickness)
            : null;

        // Render our geometry (borders and background)
        context.DrawGeometry(Background, pen, geometry);

        var renderBounds = new Rect(RenderSize);
        var renderRadius = isStroked
            ? radius.AddScalar(thickness * 0.5)
            : radius;

        // Creates the geometry that represents the surface, including the border
        RenderedGeometry = CreateGeometry(ShapeFamily, renderBounds, renderRadius, isStroked);

        OnRendered();
    }

    private CornerRadius GetActualCornerRadius(double width, double height)
    {
        CornerRadius radius;
        if (ShapeRadius is null)
        {
            radius = ShapeHelper.GetRadiusForStyle(ShapeStyle, ShapeCorner, width, height);
        }
        else
        {
            radius = ShapeHelper.ClampCornerRadius(
                ShapeRadius.Value,
                ShapeStyle,
                width,
                height,
                UseStyleOnRadiusOverride,
                UseCornersOnRadiusOverride);
        }

        return radius;
    }

    private static StreamGeometry CreateGeometry(ShapeFamily family, Rect bounds, CornerRadius radius, bool isStroked)
    {
        var geometry = new StreamGeometry();
        using (var context = geometry.Open())
        {
            if (family is ShapeFamily.Rounded)
            {
                DrawRectangle(context, bounds, radius, isStroked);
            }
            else
            {
                DrawOctagon(context, bounds, radius, isStroked);
            }
        }

        geometry.Freeze();
        return geometry;
    }

    private static void DrawRectangle(StreamGeometryContext context, Rect bounds, CornerRadius radius, bool isStroked)
    {
        var p0 = new Point(radius.TopLeft + bounds.X, bounds.Y);

        var p1 = new Point(bounds.Width + bounds.X - radius.TopRight, bounds.Y);
        var p2 = new Point(bounds.Width + bounds.X, bounds.Height + bounds.Y - radius.BottomLeft);
        var p3 = new Point(radius.BottomRight + bounds.X, bounds.Height + bounds.Y);
        var p4 = new Point(bounds.X, radius.TopLeft + bounds.Y);

        var a1 = new Point(bounds.Width + bounds.X, radius.TopRight + bounds.Y);
        var a2 = new Point(bounds.Width + bounds.X - radius.BottomLeft, bounds.Height + bounds.Y);
        var a3 = new Point(bounds.X, bounds.Height + bounds.Y - radius.BottomRight);
        var a4 = new Point(radius.TopLeft + bounds.X, bounds.Y);

        context.BeginFigure(startPoint: p0, isFilled: true, isClosed: true);

        LineTo(context, p1, isStroked);
        ArcTo(context, a1, radius.TopRight, isStroked);

        LineTo(context, p2, isStroked);
        ArcTo(context, a2, radius.BottomLeft, isStroked);

        LineTo(context, p3, isStroked);
        ArcTo(context, a3, radius.BottomRight, isStroked);

        LineTo(context, p4, isStroked);
        ArcTo(context, a4, radius.TopLeft, isStroked);
    }

    private static void DrawOctagon(StreamGeometryContext context, Rect bounds, CornerRadius radius, bool isStroked)
    {
        var p0 = new Point(radius.TopLeft + bounds.X, bounds.Y);

        var p1 = new Point(bounds.Width + bounds.X - radius.TopRight, bounds.Y);
        var p2 = new Point(bounds.Width + bounds.X, radius.TopRight + bounds.Y);
        var p3 = new Point(bounds.Width + bounds.X, bounds.Height - radius.BottomLeft + bounds.Y);
        var p4 = new Point(bounds.Width - radius.BottomLeft + bounds.X, bounds.Height + bounds.Y);
        var p5 = new Point(radius.BottomRight + bounds.X, bounds.Height + bounds.Y);
        var p6 = new Point(bounds.X, bounds.Height - radius.BottomRight + bounds.Y);
        var p7 = new Point(bounds.X, radius.TopLeft + bounds.Y);

        context.BeginFigure(p0, isFilled: true, isClosed: true);

        LineTo(context, p1, isStroked);
        LineTo(context, p2, isStroked);
        LineTo(context, p3, isStroked);
        LineTo(context, p4, isStroked);
        LineTo(context, p5, isStroked);
        LineTo(context, p6, isStroked);
        LineTo(context, p7, isStroked);
    }

    private static void LineTo(StreamGeometryContext context, Point point, bool isStroked) =>
        context.LineTo(point, isStroked, isSmoothJoin: false);

    private static void ArcTo(StreamGeometryContext context, Point point, double size, bool isStroked)
    {
        if (size > 0.0)
        {
            context.ArcTo(
                point,
                new Size(size, size),
                rotationAngle: 0.0,
                isLargeArc: false,
                sweepDirection: SweepDirection.Clockwise,
                isStroked,
                isSmoothJoin: false);
        }
    }

    protected virtual void OnRendered() => Rendered?.Invoke(this, EventArgs.Empty);
}