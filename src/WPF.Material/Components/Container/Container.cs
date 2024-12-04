namespace WPF.Material.Components;

/// <summary>
/// Provides a container for a single content.
/// </summary>
[TemplatePart(Name = "PART_Surface", Type = typeof(Surface))]
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

    /// <summary>
    /// Identifies the <see cref="BorderThicknessContributesToContentMargin"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BorderThicknessContributesToContentMarginProperty = DependencyProperty
        .Register(
            nameof(BorderThicknessContributesToContentMargin),
            typeof(bool),
            typeof(Container),
            new PropertyMetadata(false));
    
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

    private Surface surface = null!;

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
    /// Gets or sets the shape family of the container.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public ShapeFamily ShapeFamily
    {
        get => (ShapeFamily)GetValue(ShapeFamilyProperty);
        set => SetValue(ShapeFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the shape style of the container. A style can be used to apply a predefined set of corner radii.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public ShapeStyle ShapeStyle
    {
        get => (ShapeStyle)GetValue(ShapeStyleProperty);
        set => SetValue(ShapeStyleProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that specifies which corners of the container should have a radius applied.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public ShapeCorner ShapeCorner
    {
        get => (ShapeCorner)GetValue(ShapeCornerProperty);
        set => SetValue(ShapeCornerProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom radius for the container. If set when <see cref="UseCornersOnRadiusOverride"/> is set to
    /// <see cref="ShapeCorner.All"/>, the <see cref="ShapeStyle"/> will be  ignored; otherwise, the container will
    /// have a combination of the custom radius and the style radius. See <see cref="UseStyleOnRadiusOverride"/> and
    /// <see cref="UseCornersOnRadiusOverride"/> properties for possible combinations.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public CornerRadius? ShapeRadius
    {
        get => (CornerRadius?)GetValue(ShapeRadiusProperty);
        set => SetValue(ShapeRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that specifies whether the <see cref="ShapeStyle"/> should be used when a custom radius is
    /// set. If set to <see langword="true"/>, the style radius will be used in combination with the custom radius when
    /// <see cref="UseCornersOnRadiusOverride"/> is not set to <see cref="ShapeCorner.All"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public bool UseStyleOnRadiusOverride
    {
        get => (bool)GetValue(UseStyleOnRadiusOverrideProperty);
        set => SetValue(UseStyleOnRadiusOverrideProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that specifies which corners of the container should have a radius applied when a custom
    /// radius is set. If set to <see cref="ShapeCorner.All"/>, the custom radius will be applied to all corners; if
    /// set to <see cref="ShapeCorner.None"/>, the custom radius will be ignored.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public ShapeCorner UseCornersOnRadiusOverride
    {
        get => (ShapeCorner)GetValue(UseCornersOnRadiusOverrideProperty);
        set => SetValue(UseCornersOnRadiusOverrideProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that specifies whether the <see cref="Container.BorderThickness"/> should be included
    /// in the content margin calculation.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Layout)]
    public bool BorderThicknessContributesToContentMargin
    {
        get => (bool)GetValue(BorderThicknessContributesToContentMarginProperty);
        set => SetValue(BorderThicknessContributesToContentMarginProperty, value);
    }

    /// <summary>
    /// Gets a <see cref="Geometry"/> that represents the rendered container.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public Geometry RenderedGeometry
    {
        get => (Geometry)GetValue(RenderedGeometryProperty);
        private set => SetValue(RenderedGeometryPropertyKey, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        surface = GetTemplateChild(PartSurface) as Surface
                  ?? throw new NullReferenceException($"Missing required template part: {PartSurface}");

        surface.Rendered -= OnSurfaceRendered;
        surface.Rendered += OnSurfaceRendered;

        UpdateRenderedGeometry();
    }

    private void OnSurfaceRendered(object? sender, EventArgs e) => UpdateRenderedGeometry();

    private void UpdateRenderedGeometry() => RenderedGeometry = surface.RenderedGeometry;
}