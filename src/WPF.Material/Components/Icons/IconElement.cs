namespace WPF.Material.Components;

/// <summary>
/// Provides a base class for elements that render icons using a <see cref="DrawingVisual"/>.
/// </summary>
public abstract class IconElement : FrameworkElement
{
    private static readonly DependencyPropertyKey IsRenderedPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(IsRendered),
        typeof(bool),
        typeof(IconElement),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsRendered"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsRenderedProperty = IsRenderedPropertyKey.DependencyProperty;
    
    private readonly DrawingVisual visual = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="IconElement"/> class.
    /// </summary>
    protected IconElement()
    {
        AddVisualChild(visual);

        Loaded += OnLoaded;
    }

    /// <summary>
    /// Gets a value that indicates whether the icon is currently rendered.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Miscellaneous)]
    public bool IsRendered
    {
        get => (bool)GetValue(IsRenderedProperty);
        private set => SetValue(IsRenderedPropertyKey, value);
    }
    
    protected override int VisualChildrenCount => 1;

    /// <summary>
    /// Renders the icon using the specified <see cref="DrawingContext"/>.
    /// </summary>
    /// <param name="context">The <see cref="DrawingContext"/> to render the icon.</param>
    protected abstract void Render(DrawingContext context);

    /// <summary>
    /// Validates the current rendering of the icon. This method is called before rendering the icon, and must return
    /// <see langword="true"/> in order to render the icon; otherwise, the rendering is not performed.
    /// </summary>
    /// <returns>A value indicating whether the rendering is valid.</returns>
    protected virtual bool ValidateRender() => true;

    /// <summary>
    /// Invalidates the rendering of the icon, and forces it to be rendered again.
    /// </summary>
    protected void InvalidateRender()
    {
        IsRendered = false;
        RenderInternal();
    }

    protected override Visual GetVisualChild(int _) => visual;

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);

        // Invalidate the rendering, as the size of the icon may have changed.
        InvalidateRender();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        RenderInternal();
    }

    private void RenderInternal()
    {
        if (!ValidateRender())
        {
            return;
        }

        using var context = visual.RenderOpen();
        Render(context);
        
        IsRendered = true;
    }
}