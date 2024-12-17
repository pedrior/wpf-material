namespace WPF.Material.Components;

/// <summary>
/// Renders a symbol from the Google's Material Symbols font.
/// </summary>
public class SymbolIcon : FrameworkElement
{
    /// <summary>
    /// Identifies the <see cref="Brush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
        nameof(Brush),
        typeof(Brush),
        typeof(SymbolIcon),
        new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="Symbol"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(
        nameof(Symbol),
        typeof(Symbol?),
        typeof(SymbolIcon),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, null, CoerceSymbol));

    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type),
        typeof(SymbolType),
        typeof(SymbolIcon),
        new FrameworkPropertyMetadata(SymbolType.Rounded, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="Weight"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty WeightProperty = DependencyProperty.Register(
        nameof(Weight),
        typeof(FontWeight),
        typeof(SymbolIcon),
        new FrameworkPropertyMetadata(FontWeights.Regular, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="IsFilled"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsFilledProperty = DependencyProperty.Register(
        nameof(IsFilled),
        typeof(bool),
        typeof(SymbolIcon),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

    static SymbolIcon()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SymbolIcon),
            new FrameworkPropertyMetadata(typeof(SymbolIcon)));
    }

    /// <summary>
    /// Gets or sets a <see cref="Brush"/> that paints the symbol.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Brush)]
    public Brush Brush
    {
        get => (Brush)GetValue(BrushProperty);
        set => SetValue(BrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Symbol"/> to be displayed.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public Symbol? Symbol
    {
        get => (Symbol?)GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    /// <summary>
    /// Gets or sets the type of the symbol.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public SymbolType Type
    {
        get => (SymbolType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the weight or thickness of the font that renders the symbol.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public FontWeight Weight
    {
        get => (FontWeight)GetValue(WeightProperty);
        set => SetValue(WeightProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the symbol is filled. Not all symbols can be filled.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsFilled
    {
        get => (bool)GetValue(IsFilledProperty);
        set => SetValue(IsFilledProperty, value);
    }

    protected override void OnRender(DrawingContext context)
    {
        base.OnRender(context);

        var glyphTypeface = GetGlyphTypeface(Type, Weight, IsFilled);

        // It doesn't matter if the glyph index isn't found, a '.notdef' glyph will be rendered if it's the case.
        glyphTypeface.CharacterToGlyphMap.TryGetValue((int)(Symbol ?? 0), out var glyphIndex);

        var renderingEmSize = Math.Min(ActualWidth, ActualHeight);
        
        // Centers the glyph in the available space.
        var baselineOrigin = new Point(
            x: (ActualWidth - renderingEmSize) * 0.5,
            y: (ActualHeight + renderingEmSize) * 0.5);
        
        var pixelsPerDip = (float)VisualTreeHelper.GetDpi(this).PixelsPerDip;

        var symbolGlyph = BuildSymbolGlyph(
            glyphTypeface,
            glyphIndex,
            renderingEmSize,
            baselineOrigin,
            pixelsPerDip);

        context.DrawGlyphRun(Brush, symbolGlyph);
    }

    private static GlyphTypeface GetGlyphTypeface(SymbolType symbolType, FontWeight fontWeight, bool isFilled)
    {
        var fontFamily = SymbolFontsCache.GetOrLoad(symbolType, isFilled);
        var typeface = new Typeface(
            fontFamily,
            FontStyles.Normal,
            fontWeight,
            FontStretches.Normal);

        return typeface.TryGetGlyphTypeface(out var glyphTypeface)
            ? glyphTypeface
            : throw new InvalidOperationException("Unable to get the glyph typeface.");
    }

    private static GlyphRun BuildSymbolGlyph(
        GlyphTypeface glyphTypeface,
        ushort glyphIndex,
        double renderingEmSize,
        Point baselineOrigin,
        float pixelsPerDip)
    {
        return new GlyphRun(
            glyphTypeface,
            bidiLevel: 0,
            isSideways: false,
            renderingEmSize,
            pixelsPerDip,
            glyphIndices: new[] { glyphIndex },
            baselineOrigin,
            advanceWidths: new[] { renderingEmSize },
            glyphOffsets: null,
            characters: null,
            deviceFontName: null,
            clusterMap: null,
            caretStops: null,
            language: null);
    }

    private static object? CoerceSymbol(DependencyObject element, object? value) =>
        value ?? TryGetFallbackSymbolFromElement(element);

    private static object? TryGetFallbackSymbolFromElement(DependencyObject element)
    {
        // We've received a mull symbol, let's try to get a fallback symbol from the element, if any.
        // This is useful when we are using the SymbolIcon in a template of a selectable control and the
        // rest or selection icon is missing.
        return element.GetValue(SymbolProperty) ?? Icon.GetIcon(element) ?? Icon.GetIconOnSelecting(element);
    }
}