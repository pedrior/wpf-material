namespace WPF.Material.Components;

internal class SurfaceMirror : FrameworkElement
{
    public static readonly DependencyProperty BackgroundProperty = Control.BackgroundProperty.AddOwner(
        typeof(SurfaceMirror),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
    
    public static readonly DependencyProperty BorderBrushProperty = Control.BorderBrushProperty.AddOwner(
        typeof(SurfaceMirror),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
    
    public static readonly DependencyProperty BorderThicknessProperty = Control.BorderThicknessProperty.AddOwner(
        typeof(SurfaceMirror),
        new FrameworkPropertyMetadata(new Thickness(), FrameworkPropertyMetadataOptions.AffectsRender));
    
    public static readonly DependencyProperty SurfaceGeometryProperty = DependencyProperty.Register(
        nameof(SurfaceGeometry),
        typeof(Geometry),
        typeof(SurfaceMirror),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
    
    static SurfaceMirror()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SurfaceMirror),
            new FrameworkPropertyMetadata(typeof(SurfaceMirror)));
        
        IsHitTestVisibleProperty.OverrideMetadata(
            typeof(SurfaceMirror),
            new FrameworkPropertyMetadata(false));
    }
    
    public Brush? Background
    {
        get => (Brush?)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }
    
    public Brush? BorderBrush
    {
        get => (Brush?)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }
    
    public Thickness BorderThickness
    {
        get => (Thickness)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }
    
    public Geometry? SurfaceGeometry
    {
        get => (Geometry?)GetValue(SurfaceGeometryProperty);
        set => SetValue(SurfaceGeometryProperty, value);
    }
    
    protected override void OnRender(DrawingContext context)
    {
        var geometry = SurfaceGeometry;
        if (geometry is null)
        {
            return;
        }

        var pen = new Pen(BorderBrush, BorderThickness.Left);
        context.DrawGeometry(Background, pen, geometry);
    }
}