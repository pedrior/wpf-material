namespace WPF.Material.Components;

/// <summary>
/// Provides a container for a single content.
/// </summary>
[TemplatePart(Name = "PART_Surface", Type = typeof(ContainerSurface))]
public class Container : ContentControl
{
    /// <summary>
    /// Identifies the <see cref="ShapeFamily"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeFamilyProperty = DependencyProperty.Register(
        nameof(ShapeFamily),
        typeof(ShapeFamily),
        typeof(Container),
        new FrameworkPropertyMetadata(ShapeFamily.Rounded, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="ShapeStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeStyleProperty = DependencyProperty.Register(
        nameof(ShapeStyle),
        typeof(ShapeStyle),
        typeof(Container),
        new FrameworkPropertyMetadata(ShapeStyle.None, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="ShapeCorner"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeCornerProperty = DependencyProperty.Register(
        nameof(ShapeCorner),
        typeof(ShapeCorner),
        typeof(Container),
        new FrameworkPropertyMetadata(ShapeCorner.All, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="ShapeRadius"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeRadiusProperty = DependencyProperty.Register(
        nameof(ShapeRadius),
        typeof(CornerRadius?),
        typeof(Container),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="UseStyleOnRadiusOverride"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UseStyleOnRadiusOverrideProperty = DependencyProperty.Register(
        nameof(UseStyleOnRadiusOverride),
        typeof(bool),
        typeof(Container),
        new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="UseCornersOnRadiusOverride"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UseCornersOnRadiusOverrideProperty = DependencyProperty.Register(
        nameof(UseCornersOnRadiusOverride),
        typeof(ShapeCorner),
        typeof(Container),
        new FrameworkPropertyMetadata(ShapeCorner.All, FrameworkPropertyMetadataOptions.AffectsRender));

    private static readonly DependencyPropertyKey RenderedGeometryPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(RenderedGeometry),
        typeof(Geometry),
        typeof(Container),
        new FrameworkPropertyMetadata(Geometry.Empty));

    /// <summary>
    /// Identifies the <see cref="RenderedGeometry"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RenderedGeometryProperty = RenderedGeometryPropertyKey.DependencyProperty;

    private const string PartSurface = "PART_Surface";

    private ContainerSurface surface = null!;

    static Container()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Container),
            new FrameworkPropertyMetadata(typeof(Container)));

        BorderThicknessProperty.OverrideMetadata(
            typeof(Container),
            new FrameworkPropertyMetadata(new Thickness(), FrameworkPropertyMetadataOptions.AffectsArrange));
    }

    /// <summary>
    /// Gets or sets the shape family of the container. The default value is <see cref="ShapeFamily.Rounded"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public ShapeFamily ShapeFamily
    {
        get => (ShapeFamily)GetValue(ShapeFamilyProperty);
        set => SetValue(ShapeFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the shape style of the container, which applies a predefined corner radius to the container.
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
    /// Gets or sets a value that specifies which corners of the container should have a radius applied. The default value is
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
    /// Gets or sets a custom corner radius for the container. If set when <see cref="UseCornersOnRadiusOverride"/> is
    /// <see cref="ShapeCorner.All"/>, the <see cref="ShapeStyle"/> will not be applied; otherwise, the container will
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
    /// Gets or sets a value indicating which corners of the container should have a radius applied when a custom
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
    /// Gets a <see cref="Geometry"/> that represents the rendered container.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Geometry RenderedGeometry
    {
        get => (Geometry)GetValue(RenderedGeometryProperty);
        private set => SetValue(RenderedGeometryPropertyKey, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        surface = GetTemplateChild(PartSurface) as ContainerSurface
                  ?? throw new NullReferenceException($"Missing required template part: {PartSurface}");

        surface.Rendered -= OnSurfaceRendered;
        surface.Rendered += OnSurfaceRendered;

        UpdateRenderedGeometry();
    }

    private void OnSurfaceRendered(object? sender, EventArgs e) => UpdateRenderedGeometry();

    private void UpdateRenderedGeometry() => RenderedGeometry = surface.RenderedGeometry;
}