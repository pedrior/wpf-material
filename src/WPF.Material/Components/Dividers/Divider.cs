namespace WPF.Material.Components;

/// <summary>
/// Draws line given a specified <see cref="Brush"/>, <see cref="Thickness"/> and <see cref="Orientation"/>.
/// </summary>
public class Divider : FrameworkElement
{
    /// <summary>
    /// Identifies the <see cref="Brush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
        nameof(Brush),
        typeof(Brush),
        typeof(Divider),
        new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="Thickness"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
        nameof(Thickness),
        typeof(double),
        typeof(Divider),
        new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="Orientation"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
        nameof(Orientation),
        typeof(Orientation),
        typeof(Divider),
        new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="InsetStart"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty InsetStartProperty = DependencyProperty.Register(
        nameof(InsetStart),
        typeof(double),
        typeof(Divider),
        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="InsetEnd"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty InsetEndProperty = DependencyProperty.Register(
        nameof(InsetEnd),
        typeof(double),
        typeof(Divider),
        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    static Divider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Divider),
            new FrameworkPropertyMetadata(typeof(Divider)));
    }

    /// <summary>
    /// Gets or sets the <see cref="Brush"/> that paints the line. The default is <see cref="Brushes.Black"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Brush)]
    public Brush Brush
    {
        get => (Brush)GetValue(BrushProperty);
        set => SetValue(BrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the thickness of the line. The default value is 1.0.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public double Thickness
    {
        get => (double)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    /// <summary>
    /// Gets or sets the orientation of the line. The default value is <see cref="Orientation.Horizontal"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>
    /// Gets or sets the amount of space between the start of the line and the start of the control. The default value
    /// is 0.0.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public double InsetStart
    {
        get => (double)GetValue(InsetStartProperty);
        set => SetValue(InsetStartProperty, value);
    }

    /// <summary>
    /// Gets or sets the amount of space between the end of the line and the end of the control. The default value is
    /// 0.0.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public double InsetEnd
    {
        get => (double)GetValue(InsetEndProperty);
        set => SetValue(InsetEndProperty, value);
    }

    protected override void OnRender(DrawingContext context)
    {
        var thickness = Thickness;
        var halfThickness = thickness * 0.5;

        Point p0, p1;
        if (Orientation is Orientation.Horizontal)
        {
            p0 = new Point(InsetStart, halfThickness);
            p1 = new Point(ActualWidth - InsetEnd, halfThickness);
        }
        else
        {
            p0 = new Point(halfThickness, InsetStart);
            p1 = new Point(halfThickness, ActualHeight - InsetEnd);
        }
        
        var pen = new Pen(Brush, thickness);
        context.DrawLine(pen, p0, p1);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        return Orientation is Orientation.Horizontal
            ? new Size(Math.Min(availableSize.Width, 0.0), Thickness)
            : new Size(Thickness, Math.Min(availableSize.Height, 0.0));
    }
}