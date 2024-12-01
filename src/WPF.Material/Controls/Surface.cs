using WPF.Material.Assists;
using WPF.Material.Shapes;

namespace WPF.Material.Controls;

internal class Surface : FrameworkElement
{
    public static readonly DependencyProperty BackgroundProperty = Control.BackgroundProperty.AddOwner(
        typeof(Surface),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty BorderThicknessProperty = Control.BorderThicknessProperty.AddOwner(
        typeof(Surface),
        new FrameworkPropertyMetadata(new Thickness(), FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty ShapeFamilyProperty = DependencyProperty.Register(
        nameof(ShapeFamily),
        typeof(ShapeFamily),
        typeof(Surface),
        new FrameworkPropertyMetadata(ShapeFamily.Rounded, FrameworkPropertyMetadataOptions.AffectsRender));
    
    public static readonly DependencyProperty ShapeStyleProperty = DependencyProperty.Register(
        nameof(ShapeStyle),
        typeof(ShapeStyle),
        typeof(Surface),
        new FrameworkPropertyMetadata(ShapeStyle.None, FrameworkPropertyMetadataOptions.AffectsRender));
    
    public static readonly DependencyProperty ShapeCornerProperty = DependencyProperty.Register(
        nameof(ShapeCorner),
        typeof(ShapeCorner),
        typeof(Surface),
        new FrameworkPropertyMetadata(ShapeCorner.All, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty ShapeRadiusProperty = DependencyProperty.Register(
        nameof(ShapeRadius),
        typeof(CornerRadius?),
        typeof(Surface),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
    
    private static readonly DependencyPropertyKey RenderedGeometryPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(RenderedGeometry),
        typeof(Geometry),
        typeof(Surface),
        new PropertyMetadata(Geometry.Empty));

    public static readonly DependencyProperty RenderedGeometryProperty = RenderedGeometryPropertyKey.DependencyProperty;

    private static readonly RoutedEvent RenderedEvent  = EventManager.RegisterRoutedEvent(
        nameof(Rendered),
        RoutingStrategy.Bubble, 
        typeof(RoutedEventHandler),
        typeof(Surface)); 

    public Brush? Background
    {
        get => (Brush)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }
    
    public Thickness BorderThickness
    {
        get => (Thickness)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }
    
    public ShapeFamily ShapeFamily
    {
        get => (ShapeFamily)GetValue(ShapeFamilyProperty);
        set => SetValue(ShapeFamilyProperty, value);
    }

    public ShapeStyle ShapeStyle
    {
        get => (ShapeStyle)GetValue(ShapeStyleProperty);
        set => SetValue(ShapeStyleProperty, value);
    }
    
    public ShapeCorner ShapeCorner
    {
        get => (ShapeCorner)GetValue(ShapeCornerProperty);
        set => SetValue(ShapeCornerProperty, value);
    }

    public CornerRadius? ShapeRadius
    {
        get => (CornerRadius?)GetValue(ShapeRadiusProperty);
        set => SetValue(ShapeRadiusProperty, value);
    }

    public Geometry RenderedGeometry
    {
        get => (Geometry)GetValue(RenderedGeometryProperty);
        private set => SetValue(RenderedGeometryPropertyKey, value);
    }

    public event RoutedEventHandler Rendered
    {
        add => AddHandler(RenderedEvent, value);
        remove => RemoveHandler(RenderedEvent, value);
    }

    protected override void OnRender(DrawingContext context)
    {
        var width = ActualWidth;
        var height = ActualHeight;
        
        if (width is 0.0 || height is 0.0)
        {
            // Nothing to render
            return;
        }
        
        var thickness = Math.Abs(BorderThickness.Left);
        var halfThickness = thickness * 0.5;
        
        var isStroked = thickness >= 0.0;
        
        var innerWidth = Math.Abs(width - thickness);
        var innerHeight = Math.Abs(height - thickness);

        var bounds = new Rect(
            halfThickness,
            halfThickness,
            innerWidth,
            innerHeight);
        
        var radius = ShapeRadius is not null
            ? ShapeHelper.ClampCornerRadius(
                ShapeRadius.Value,
                ShapeStyle,
                innerWidth,
                innerHeight,
                useStyleRadius: ShapeAssist.GetUseStyleOnRadiusOverride(this),
                cornersOverride: ShapeAssist.GetUseCornersOnRadiusOverride(this))
            : ShapeHelper.GetRadiusForStyle(ShapeStyle, ShapeCorner, innerWidth, innerHeight);

        var pen = isStroked ? new Pen(null, thickness) : null;
        var geometry = CreateGeometry(ShapeFamily, bounds, radius, isStroked);

        context.DrawGeometry(Background, pen, geometry);

        RenderedGeometry = geometry;
        OnRendered(new RoutedEventArgs(RenderedEvent));
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

        context.BeginFigure(p0, isFilled: true, isStroked);
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
        if (Math.Abs(size) >= double.Epsilon)
        {
            context.ArcTo(
                point,
                new Size(size, size),
                rotationAngle: 0,
                isLargeArc: false,
                sweepDirection: SweepDirection.Clockwise,
                isStroked,
                isSmoothJoin: false);
        }
    }
    
    private void OnRendered(RoutedEventArgs e) => RaiseEvent(e);
}