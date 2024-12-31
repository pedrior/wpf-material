namespace WPF.Material.Components;

/// <summary>
/// Renders an icon from the Google's Material Symbols font based on a specified state.
/// <remarks>
/// This element is intended to be used in interactive scenarios where the icon can change its appearance based on the
/// user's interaction. You can configure the icon's appearance for each state using the attached properties defined
/// in the <see cref="Icon"/> class.
/// </remarks>
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public class InteractiveIcon : IconElement
{
    /// <summary>
    /// Identifies the <see cref="Brush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
        nameof(Brush),
        typeof(Brush),
        typeof(InteractiveIcon),
        new PropertyMetadata(Brushes.Black, (e, _) => ((InteractiveIcon)e).InvalidateRender()));

    /// <summary>
    /// Identifies the <see cref="State"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
        nameof(State),
        typeof(SymbolState),
        typeof(InteractiveIcon),
        new PropertyMetadata(SymbolState.Rest, (e, _) => ((InteractiveIcon)e).InvalidateRender()));

    private static readonly DependencyProperty[] IconProperties =
    {
        Icon.RestSymbolProperty,
        Icon.HoverSymbolProperty,
        Icon.PressSymbolProperty,
        Icon.SelectionSymbolProperty,
        Icon.RestSymbolFallbackProperty,
        Icon.HoverSymbolFallbackProperty,
        Icon.PressSymbolFallbackProperty,
        Icon.SelectionSymbolFallbackProperty,
        Icon.RestSymbolWeightProperty,
        Icon.HoverSymbolWeightProperty,
        Icon.PressSymbolWeightProperty,
        Icon.SelectionSymbolWeightProperty,
        Icon.IsRestSymbolFilledProperty,
        Icon.IsHoverSymbolFilledProperty,
        Icon.IsPressSymbolFilledProperty,
        Icon.IsSelectionSymbolFilledProperty
    };

    // These variables hold the symbol and state that is about to be rendered
    private Symbol renderingSymbol = Icon.NotDefSymbol;
    private SymbolState renderingSymbolState = SymbolState.None;

    static InteractiveIcon()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(InteractiveIcon),
            new FrameworkPropertyMetadata(typeof(InteractiveIcon)));

        OverrideIconAttachedProperties();
    }

    /// <summary>
    /// Gets or sets a <see cref="Brush"/> that paints the symbol. The default value is <see cref="Brushes.Black"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Brush)]
    public Brush Brush
    {
        get => (Brush)GetValue(BrushProperty);
        set => SetValue(BrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the current state that determines the icon to render. The default value is
    /// <see cref="SymbolState.Rest"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public SymbolState State
    {
        get => (SymbolState)GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    protected override bool ValidateRender()
    {
        renderingSymbolState = State;
        renderingSymbol = GetSymbol(renderingSymbolState)
                          ?? ResolveSymbolFallbacks(renderingSymbolState);

        return renderingSymbol is not Icon.NotDefSymbol;
    }

    protected override void Render(DrawingContext context)
    {
        SymbolRender.RenderSymbolAsGlyph(
            context,
            ActualWidth,
            ActualHeight,
            renderingSymbol,
            Icon.GetDefaultStyle(this),
            GetSymbolWeight(renderingSymbolState),
            Brush,
            GetSymbolIsFilled(renderingSymbolState),
            VisualTreeHelper.GetDpi(this).PixelsPerDip);
    }

    private static void OverrideIconAttachedProperties()
    {
        // Override the metadata of the Icon attached properties to invalidate the rendering when they change.
        foreach (var property in IconProperties)
        {
            property.OverrideMetadata(
                typeof(InteractiveIcon),
                new FrameworkPropertyMetadata(
                    property.DefaultMetadata.DefaultValue,
                    (e, _) => ((InteractiveIcon)e).InvalidateRender()));
        }
    }

    private FontWeight GetSymbolWeight(SymbolState state) => state switch
    {
        SymbolState.Rest => Icon.GetRestSymbolWeight(this),
        SymbolState.Hover => Icon.GetHoverSymbolWeight(this),
        SymbolState.Press => Icon.GetPressSymbolWeight(this),
        SymbolState.Select => Icon.GetSelectionSymbolWeight(this),
        _ => FontWeights.Normal
    };

    private bool GetSymbolIsFilled(SymbolState state) => state switch
    {
        SymbolState.Rest => Icon.GetIsRestSymbolFilled(this),
        SymbolState.Hover => Icon.GetIsHoverSymbolFilled(this),
        SymbolState.Press => Icon.GetIsPressSymbolFilled(this),
        SymbolState.Select => Icon.GetIsSelectionSymbolFilled(this),
        _ => false
    };

    private Symbol ResolveSymbolFallbacks(SymbolState state)
    {
        var visited = new HashSet<SymbolState>(5);

        while (true)
        {
            // Keep track of the visited states to avoid circular fallbacks
            if (!visited.Add(state))
            {
                return Icon.NotDefSymbol;
            }

            if (GetSymbol(state) is { } symbol)
            {
                // Found! Return it.
                return symbol;
            }

            // Try finding a fallback for the current state
            state = GetSymbolFallbackState(state);

            // We found a state that has no fallbacks, so we return the NotDefSymbol, the render validation will
            // handle this case by skipping the rendering.
            if (state is SymbolState.None)
            {
                return Icon.NotDefSymbol;
            }
        }
    }

    private Symbol? GetSymbol(SymbolState state) => state switch
    {
        SymbolState.Rest => Icon.GetRestSymbol(this),
        SymbolState.Hover => Icon.GetHoverSymbol(this),
        SymbolState.Press => Icon.GetPressSymbol(this),
        SymbolState.Select => Icon.GetSelectionSymbol(this),
        _ => Icon.NotDefSymbol
    };

    private SymbolState GetSymbolFallbackState(SymbolState state) => state switch
    {
        SymbolState.Rest => Icon.GetRestSymbolFallback(this),
        SymbolState.Hover => Icon.GetHoverSymbolFallback(this),
        SymbolState.Press => Icon.GetPressSymbolFallback(this),
        SymbolState.Select => Icon.GetSelectionSymbolFallback(this),
        _ => SymbolState.None
    };
}