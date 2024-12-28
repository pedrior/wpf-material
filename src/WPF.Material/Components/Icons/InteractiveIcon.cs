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
public class InteractiveIcon : FrameworkElement
{
    /// <summary>
    /// Identifies the <see cref="Brush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
        nameof(Brush),
        typeof(Brush),
        typeof(InteractiveIcon),
        new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="State"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
        nameof(State),
        typeof(SymbolState),
        typeof(InteractiveIcon),
        new FrameworkPropertyMetadata(SymbolState.Rest, FrameworkPropertyMetadataOptions.AffectsRender));

    private static readonly DependencyPropertyKey IsSymbolVisiblePropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(IsSymbolVisible),
        typeof(bool),
        typeof(InteractiveIcon),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsSymbolVisible"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsSymbolVisibleProperty = IsSymbolVisiblePropertyKey.DependencyProperty;

    private readonly HashSet<SymbolState> visitedStates = new(5);

    static InteractiveIcon()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(InteractiveIcon),
            new FrameworkPropertyMetadata(typeof(InteractiveIcon)));

        DependencyProperty[] IconProperties =
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
        
        foreach (var property in IconProperties)
        {
            property.OverrideMetadata(
                typeof(InteractiveIcon),
                new FrameworkPropertyMetadata(
                    property.DefaultMetadata.DefaultValue,
                    FrameworkPropertyMetadataOptions.AffectsRender));
        }
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

    /// <summary>
    /// Gets a value that indicates whether the symbol is currently visible or rendered.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Miscellaneous)]
    public bool IsSymbolVisible
    {
        get => (bool)GetValue(IsSymbolVisibleProperty);
        private set => SetValue(IsSymbolVisiblePropertyKey, value);
    }

    protected override void OnRender(DrawingContext context)
    {
        base.OnRender(context);

        var state = State;
        var symbol = GetSymbol(state) ?? ResolveSymbolFallbacks(state);

        if (symbol is Icon.NotDefSymbol)
        {
            IsSymbolVisible = false;
            return;
        }

        SymbolRender.RenderSymbolAsGlyph(
            context,
            ActualWidth,
            ActualHeight,
            symbol,
            Icon.GetDefaultStyle(this),
            GetSymbolWeight(state),
            Brush,
            GetSymbolIsFilled(state),
            VisualTreeHelper.GetDpi(this).PixelsPerDip);

        IsSymbolVisible = true;
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
        visitedStates.Clear();
        while (true)
        {
            // Avoid infinite loop
            if (!visitedStates.Add(state))
            {
                return Icon.NotDefSymbol;
            }

            if (GetSymbol(state) is { } symbol)
            {
                return symbol;
            }

            state = GetSymbolFallbackState(state);
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