namespace WPF.Material.Components;

/// <summary>
/// Renders an icon from the Google's Material Symbols font.
/// </summary>
public class Icon : FrameworkElement
{
    /// <summary>
    /// Identifies the <see cref="Brush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
        nameof(Brush),
        typeof(Brush),
        typeof(Icon),
        new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="Symbol"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(
        nameof(Symbol),
        typeof(Symbol?),
        typeof(Icon),
        new FrameworkPropertyMetadata(NotDefSymbol, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type),
        typeof(SymbolType),
        typeof(Icon),
        new FrameworkPropertyMetadata(SymbolType.Rounded, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="Weight"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty WeightProperty = DependencyProperty.Register(
        nameof(Weight),
        typeof(FontWeight),
        typeof(Icon),
        new FrameworkPropertyMetadata(FontWeights.Regular, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the <see cref="IsFilled"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsFilledProperty = DependencyProperty.Register(
        nameof(IsFilled),
        typeof(bool),
        typeof(Icon),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifies the Size attached property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty = DependencyProperty.RegisterAttached(
        "Size",
        typeof(double),
        typeof(Icon),
        new PropertyMetadata(18.0, null, CoerceSize));

    /// <summary>
    /// Identifies the DefaultStyle attached property.
    /// </summary>
    public static readonly DependencyProperty DefaultStyleProperty = DependencyProperty.RegisterAttached(
        "DefaultStyle",
        typeof(SymbolType),
        typeof(Icon),
        new FrameworkPropertyMetadata(SymbolType.Rounded, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the RestSymbol attached property.
    /// </summary>
    public static readonly DependencyProperty RestSymbolProperty = DependencyProperty.RegisterAttached(
        "RestSymbol",
        typeof(Symbol?),
        typeof(Icon),
        new PropertyMetadata(null));

    /// <summary>
    /// Identifies the HoverSymbol attached property.
    /// </summary>
    public static readonly DependencyProperty HoverSymbolProperty = DependencyProperty.RegisterAttached(
        "HoverSymbol",
        typeof(Symbol?),
        typeof(Icon),
        new PropertyMetadata(null));

    /// <summary>
    /// Identifies the PressSymbol attached property.
    /// </summary>
    public static readonly DependencyProperty PressSymbolProperty = DependencyProperty.RegisterAttached(
        "PressSymbol",
        typeof(Symbol?),
        typeof(Icon),
        new PropertyMetadata(null));

    /// <summary>
    /// Identifies the SelectionSymbol attached property.
    /// </summary>
    public static readonly DependencyProperty SelectionSymbolProperty = DependencyProperty.RegisterAttached(
        "SelectionSymbol",
        typeof(Symbol?),
        typeof(Icon),
        new PropertyMetadata(null));

    /// <summary>
    /// Identifies the RestSymbolFallback attached property.
    /// </summary>
    public static readonly DependencyProperty RestSymbolFallbackProperty = DependencyProperty.RegisterAttached(
        "RestSymbolFallback",
        typeof(SymbolState),
        typeof(Icon),
        new PropertyMetadata(SymbolState.None));

    /// <summary>
    /// Identifies the HoverSymbolFallback attached property.
    /// </summary>
    public static readonly DependencyProperty HoverSymbolFallbackProperty = DependencyProperty.RegisterAttached(
        "HoverSymbolFallback",
        typeof(SymbolState),
        typeof(Icon),
        new PropertyMetadata(SymbolState.Rest));

    /// <summary>
    /// Identifies the PressSymbolFallback attached property.
    /// </summary>
    public static readonly DependencyProperty PressSymbolFallbackProperty = DependencyProperty.RegisterAttached(
        "PressSymbolFallback",
        typeof(SymbolState),
        typeof(Icon),
        new PropertyMetadata(SymbolState.Rest));

    /// <summary>
    /// Identifies the SelectionSymbolFallback attached property.
    /// </summary>
    public static readonly DependencyProperty SelectionSymbolFallbackProperty = DependencyProperty.RegisterAttached(
        "SelectionSymbolFallback",
        typeof(SymbolState),
        typeof(Icon),
        new PropertyMetadata(SymbolState.Rest));

    /// <summary>
    /// Identifies the PressSymbolFallback attached property.
    /// </summary>
    public static readonly DependencyProperty IsRestSymbolFilledProperty = DependencyProperty.RegisterAttached(
        "IsRestSymbolFilled",
        typeof(bool),
        typeof(Icon),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the IsHoverSymbolFilled attached property.
    /// </summary>
    public static readonly DependencyProperty IsHoverSymbolFilledProperty = DependencyProperty.RegisterAttached(
        "IsHoverSymbolFilled",
        typeof(bool),
        typeof(Icon),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the IsPressSymbolFilled attached property.
    /// </summary>
    public static readonly DependencyProperty IsPressSymbolFilledProperty = DependencyProperty.RegisterAttached(
        "IsPressSymbolFilled",
        typeof(bool),
        typeof(Icon),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the IsSelectionSymbolFilled attached property.
    /// </summary>
    public static readonly DependencyProperty IsSelectionSymbolFilledProperty = DependencyProperty.RegisterAttached(
        "IsSelectionSymbolFilled",
        typeof(bool),
        typeof(Icon),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the RestSymbolWeight attached property.
    /// </summary>
    public static readonly DependencyProperty RestSymbolWeightProperty = DependencyProperty.RegisterAttached(
        "RestSymbolWeight",
        typeof(FontWeight),
        typeof(Icon),
        new PropertyMetadata(FontWeights.Normal));

    /// <summary>
    /// Identifies the HoverSymbolWeight attached property.
    /// </summary>
    public static readonly DependencyProperty HoverSymbolWeightProperty = DependencyProperty.RegisterAttached(
        "HoverSymbolWeight",
        typeof(FontWeight),
        typeof(Icon),
        new PropertyMetadata(FontWeights.Normal));

    /// <summary>
    /// Identifies the PressSymbolWeight attached property.
    /// </summary>
    public static readonly DependencyProperty PressSymbolWeightProperty = DependencyProperty.RegisterAttached(
        "PressSymbolWeight",
        typeof(FontWeight),
        typeof(Icon),
        new PropertyMetadata(FontWeights.Normal));

    /// <summary>
    /// Identifies the SelectionSymbolWeight attached property.
    /// </summary>
    public static readonly DependencyProperty SelectionSymbolWeightProperty = DependencyProperty.RegisterAttached(
        "SelectionSymbolWeight",
        typeof(FontWeight),
        typeof(Icon),
        new PropertyMetadata(FontWeights.Normal));

    public const Symbol NotDefSymbol = 0;

    static Icon()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Icon),
            new FrameworkPropertyMetadata(typeof(Icon)));
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
    /// Gets or sets the <see cref="Symbol"/> to be rendered. The default value is <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public Symbol? Symbol
    {
        get => (Symbol?)GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    /// <summary>
    /// Gets or sets the type of the symbol. The default value is <see cref="SymbolType.Rounded"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public SymbolType Type
    {
        get => (SymbolType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the weight or thickness of the font that renders the symbol. The default value is
    /// <see cref="FontWeights.Regular"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public FontWeight Weight
    {
        get => (FontWeight)GetValue(WeightProperty);
        set => SetValue(WeightProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the symbol is filled. Not all symbols can be filled. The default
    /// value is <see langword="false"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public bool IsFilled
    {
        get => (bool)GetValue(IsFilledProperty);
        set => SetValue(IsFilledProperty, value);
    }

    /// <summary>
    /// Sets the value of the <see cref="SizeProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="SizeProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetSize(DependencyObject element, double value) => element.SetValue(SizeProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="SizeProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="SizeProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="SizeProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static double GetSize(DependencyObject element) => (double)element.GetValue(SizeProperty);

    /// <summary>
    /// Sets the value of the <see cref="DefaultStyleProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="DefaultStyleProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetDefaultStyle(DependencyObject element, SymbolType value) =>
        element.SetValue(DefaultStyleProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="DefaultStyleProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="DefaultStyleProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="DefaultStyleProperty"/> attached property on the specified dependency
    /// object.
    /// </returns>
    public static SymbolType GetDefaultStyle(DependencyObject element) =>
        (SymbolType)element.GetValue(DefaultStyleProperty);

    /// <summary>
    /// Sets the value of the <see cref="RestSymbolProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="RestSymbolProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetRestSymbol(DependencyObject element, Symbol? value) =>
        element.SetValue(RestSymbolProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="RestSymbolProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="RestSymbolProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="RestSymbolProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static Symbol? GetRestSymbol(DependencyObject element) => (Symbol?)element.GetValue(RestSymbolProperty);

    /// <summary>
    /// Sets the value of the <see cref="HoverSymbolProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="HoverSymbolProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetHoverSymbol(DependencyObject element, Symbol? value) =>
        element.SetValue(HoverSymbolProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="HoverSymbolProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="HoverSymbolProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="HoverSymbolProperty"/> attached property on the specified dependency
    /// object.
    /// </returns>
    public static Symbol? GetHoverSymbol(DependencyObject element) => (Symbol?)element.GetValue(HoverSymbolProperty);

    /// <summary>
    /// Sets the value of the <see cref="PressSymbolProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="PressSymbolProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetPressSymbol(DependencyObject element, Symbol? value) =>
        element.SetValue(PressSymbolProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="PressSymbolProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="PressSymbolProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="PressSymbolProperty"/> attached property on the specified dependency
    /// object.
    /// </returns>
    public static Symbol? GetPressSymbol(DependencyObject element) => (Symbol?)element.GetValue(PressSymbolProperty);

    /// <summary>
    /// Sets the value of the <see cref="SelectionSymbolProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="SelectionSymbolProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetSelectionSymbol(DependencyObject element, Symbol? value) =>
        element.SetValue(SelectionSymbolProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="SelectionSymbolProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="SelectionSymbolProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="SelectionSymbolProperty"/> attached property on the specified dependency
    /// object.
    /// </returns>
    public static Symbol? GetSelectionSymbol(DependencyObject element) =>
        (Symbol?)element.GetValue(SelectionSymbolProperty);

    /// <summary>
    /// Sets the value of the <see cref="RestSymbolFallbackProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="RestSymbolFallbackProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetRestSymbolFallback(DependencyObject element, SymbolState value) =>
        element.SetValue(RestSymbolFallbackProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="RestSymbolFallbackProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="RestSymbolFallbackProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="RestSymbolFallbackProperty"/> attached property on the specified dependency
    /// object.
    /// </returns>
    public static SymbolState GetRestSymbolFallback(DependencyObject element) =>
        (SymbolState)element.GetValue(RestSymbolFallbackProperty);

    /// <summary>
    /// Sets the value of the <see cref="HoverSymbolFallbackProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="HoverSymbolFallbackProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetHoverSymbolFallback(DependencyObject element, SymbolState value) =>
        element.SetValue(HoverSymbolFallbackProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="HoverSymbolFallbackProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="HoverSymbolFallbackProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="HoverSymbolFallbackProperty"/> attached property on the specified
    /// dependency object.
    /// </returns>
    public static SymbolState GetHoverSymbolFallback(DependencyObject element) =>
        (SymbolState)element.GetValue(HoverSymbolFallbackProperty);

    /// <summary>
    /// Sets the value of the <see cref="PressSymbolFallbackProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="PressSymbolFallbackProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetPressSymbolFallback(DependencyObject element, SymbolState value) =>
        element.SetValue(PressSymbolFallbackProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="PressSymbolFallbackProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="PressSymbolFallbackProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="PressSymbolFallbackProperty"/> attached property on the specified
    /// dependency object.
    /// </returns>
    public static SymbolState GetPressSymbolFallback(DependencyObject element) =>
        (SymbolState)element.GetValue(PressSymbolFallbackProperty);

    /// <summary>
    /// Sets the value of the <see cref="SelectionSymbolFallbackProperty"/> attached property for a specified
    /// dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="SelectionSymbolFallbackProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetSelectionSymbolFallback(DependencyObject element, SymbolState value) =>
        element.SetValue(SelectionSymbolFallbackProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="SelectionSymbolFallbackProperty"/> attached property for a specified
    /// dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="SelectionSymbolFallbackProperty"/>
    /// property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="SelectionSymbolFallbackProperty"/> attached property on the specified
    /// dependency object.
    /// </returns>
    public static SymbolState GetSelectionSymbolFallback(DependencyObject element) =>
        (SymbolState)element.GetValue(SelectionSymbolFallbackProperty);

    /// <summary>
    /// Sets the value of the <see cref="IsRestSymbolFilledProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsRestSymbolFilledProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsRestSymbolFilled(DependencyObject element, bool value) =>
        element.SetValue(IsRestSymbolFilledProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsRestSymbolFilledProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsRestSymbolFilledProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsRestSymbolFilledProperty"/> attached property on the specified dependency
    /// object.
    /// </returns>
    public static bool GetIsRestSymbolFilled(DependencyObject element) =>
        (bool)element.GetValue(IsRestSymbolFilledProperty);

    /// <summary>
    /// Sets the value of the <see cref="IsHoverSymbolFilledProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsHoverSymbolFilledProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsHoverSymbolFilled(DependencyObject element, bool value) =>
        element.SetValue(IsHoverSymbolFilledProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsHoverSymbolFilledProperty"/> attached property for a specified dependenc
    /// y object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsHoverSymbolFilledProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsHoverSymbolFilledProperty"/> attached property on the specified
    /// dependency object.
    /// </returns>
    public static bool GetIsHoverSymbolFilled(DependencyObject element) =>
        (bool)element.GetValue(IsHoverSymbolFilledProperty);

    /// <summary>
    /// Sets the value of the <see cref="IsPressSymbolFilledProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsPressSymbolFilledProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsPressSymbolFilled(DependencyObject element, bool value) =>
        element.SetValue(IsPressSymbolFilledProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsPressSymbolFilledProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsPressSymbolFilledProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsPressSymbolFilledProperty"/> attached property on the specified
    /// dependency object.
    /// </returns>
    public static bool GetIsPressSymbolFilled(DependencyObject element) =>
        (bool)element.GetValue(IsPressSymbolFilledProperty);

    /// <summary>
    /// Sets the value of the <see cref="IsSelectionSymbolFilledProperty"/> attached property for a specified
    /// dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsSelectionSymbolFilledProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsSelectionSymbolFilled(DependencyObject element, bool value) =>
        element.SetValue(IsSelectionSymbolFilledProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsSelectionSymbolFilledProperty"/> attached property for a specified
    /// dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsSelectionSymbolFilledProperty"/>
    /// property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsSelectionSymbolFilledProperty"/> attached property on the specified
    /// dependency object.
    /// </returns>
    public static bool GetIsSelectionSymbolFilled(DependencyObject element) =>
        (bool)element.GetValue(IsSelectionSymbolFilledProperty);

    /// <summary>
    /// Sets the value of the <see cref="RestSymbolWeightProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="RestSymbolWeightProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetRestSymbolWeight(DependencyObject element, FontWeight value) =>
        element.SetValue(RestSymbolWeightProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="RestSymbolWeightProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="RestSymbolWeightProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="RestSymbolWeightProperty"/> attached property on the specified dependency
    /// object.
    /// </returns>
    public static FontWeight GetRestSymbolWeight(DependencyObject element) =>
        (FontWeight)element.GetValue(RestSymbolWeightProperty);

    /// <summary>
    /// Sets the value of the <see cref="HoverSymbolWeightProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="HoverSymbolWeightProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetHoverSymbolWeight(DependencyObject element, FontWeight value) =>
        element.SetValue(HoverSymbolWeightProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="HoverSymbolWeightProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="HoverSymbolWeightProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="HoverSymbolWeightProperty"/> attached property on the specified dependency
    /// object.
    /// </returns>
    public static FontWeight GetHoverSymbolWeight(DependencyObject element) =>
        (FontWeight)element.GetValue(HoverSymbolWeightProperty);

    /// <summary>
    /// Sets the value of the <see cref="PressSymbolWeightProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="PressSymbolWeightProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetPressSymbolWeight(DependencyObject element, FontWeight value) =>
        element.SetValue(PressSymbolWeightProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="PressSymbolWeightProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="PressSymbolWeightProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="PressSymbolWeightProperty"/> attached property on the specified dependency
    /// object.
    /// </returns>
    public static FontWeight GetPressSymbolWeight(DependencyObject element) =>
        (FontWeight)element.GetValue(PressSymbolWeightProperty);

    /// <summary>
    /// Sets the value of the <see cref="SelectionSymbolWeightProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="SelectionSymbolWeightProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetSelectionSymbolWeight(DependencyObject element, FontWeight value) =>
        element.SetValue(SelectionSymbolWeightProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="SelectionSymbolWeightProperty"/> attached property for a specified dependency
    /// object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="SelectionSymbolWeightProperty"/>
    /// property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="SelectionSymbolWeightProperty"/> attached property on the specified
    /// dependency object.
    /// </returns>
    public static FontWeight GetSelectionSymbolWeight(DependencyObject element) =>
        (FontWeight)element.GetValue(SelectionSymbolWeightProperty);

    protected override void OnRender(DrawingContext context)
    {
        base.OnRender(context);

        SymbolRender.RenderSymbolAsGlyph(
            context,
            ActualWidth,
            ActualHeight,
            Symbol ?? NotDefSymbol,
            Type,
            Weight,
            Brush,
            IsFilled,
            VisualTreeHelper.GetDpi(this).PixelsPerDip);
    }
    
    private static object CoerceSize(DependencyObject element, object value) => Math.Max(0.0, (double)value);
}