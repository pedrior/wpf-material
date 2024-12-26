namespace WPF.Material.Components;

/// <summary>
/// Expresses the interaction state of a UI element through a semi-transparent layer. The layer can be applied to
/// elements of any shape.
/// </summary>
public class StateLayer : FrameworkElement
{
    /// <summary>
    /// Identifies the <see cref="Brush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
        nameof(Brush),
        typeof(Brush),
        typeof(StateLayer),
        new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="State"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
        nameof(State),
        typeof(InteractionState),
        typeof(StateLayer),
        new PropertyMetadata(InteractionState.None, OnStateChanged));

    /// <summary>
    /// Identifies the <see cref="OutlineGeometry"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OutlineGeometryProperty = DependencyProperty.Register(
        nameof(OutlineGeometry),
        typeof(Geometry),
        typeof(StateLayer),
        new FrameworkPropertyMetadata(Geometry.Empty, FrameworkPropertyMetadataOptions.AffectsRender));

    static StateLayer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(StateLayer),
            new FrameworkPropertyMetadata(typeof(StateLayer)));
        
        OpacityProperty.OverrideMetadata(
            typeof(StateLayer),
            new UIPropertyMetadata(0.0));
    }
    
    /// <summary>
    /// Gets or sets the <see cref="Brush"/> that fills the layer.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Brush)]
    public Brush? Brush
    {
        get => (Brush?)GetValue(BrushProperty);
        set => SetValue(BrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="InteractionState"/> that the layer is expressing.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public InteractionState State
    {
        get => (InteractionState)GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Geometry"/> that outlines the UI element for which the state is being expressed.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public Geometry? OutlineGeometry
    {
        get => (Geometry?)GetValue(OutlineGeometryProperty);
        set => SetValue(OutlineGeometryProperty, value);
    }

    protected override void OnRender(DrawingContext context)
    {
        base.OnRender(context);

        var outline = OutlineGeometry;
        if (outline is { Bounds.IsEmpty: false })
        {
            context.DrawGeometry(Brush, null, outline);
        }
    }

    private void UpdateOpacity() => SetCurrentValue(OpacityProperty, GetStateOpacity(State));

    private static void OnStateChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((StateLayer)element).UpdateOpacity();

    private static double GetStateOpacity(InteractionState state) => state switch
    {
        InteractionState.Hovered => 0.08,
        InteractionState.Focused => 0.10,
        InteractionState.Pressed => 0.10,
        InteractionState.Dragged => 0.16,
        _ => 0.0
    };
}