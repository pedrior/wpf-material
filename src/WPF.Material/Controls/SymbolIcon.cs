using WPF.Material.Assists;
using WPF.Material.Environment;

namespace WPF.Material.Controls;

/// <summary>
/// Represents a control that displays a <see cref="Controls.Symbol"/> as an icon.
/// </summary>
public class SymbolIcon : Control
{
    /// <summary>
    /// Identifies the <see cref="Symbol"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(
        nameof(Symbol),
        typeof(Symbol?),
        typeof(SymbolIcon),
        new PropertyMetadata(null, null, CoerceSymbol));

    /// <summary>
    /// Identifies the <see cref="SymbolStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SymbolStyleProperty = DependencyProperty.Register(
        nameof(SymbolStyle),
        typeof(SymbolStyle),
        typeof(SymbolIcon),
        new PropertyMetadata(SymbolStyle.Rounded));

    /// <summary>
    /// Identifies the <see cref="IsFilled"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsFilledProperty = DependencyProperty.Register(
        nameof(IsFilled),
        typeof(bool),
        typeof(SymbolIcon),
        new PropertyMetadata(false));

    /// <summary>
    /// Initializes static members of the <see cref="SymbolIcon"/> class.
    /// </summary>
    static SymbolIcon()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SymbolIcon),
            new FrameworkPropertyMetadata(typeof(SymbolIcon)));
    }

    /// <summary>
    /// Gets or sets the symbol to display.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public Symbol? Symbol
    {
        get => (Symbol?)GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    /// <summary>
    /// Gets or sets the style of the symbol.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public SymbolStyle SymbolStyle
    {
        get => (SymbolStyle)GetValue(SymbolStyleProperty);
        set => SetValue(SymbolStyleProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the symbol is filled.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsFilled
    {
        get => (bool)GetValue(IsFilledProperty);
        set => SetValue(IsFilledProperty, value);
    }

    private static object? CoerceSymbol(DependencyObject element, object? value)
    {
        // If the incoming symbol is null, we try to get the symbol previously set. If the symbol is still null,
        // try to get the fallback symbol from the IconAssist attached properties.
        var symbol = value ?? element.GetValue(SymbolProperty);

        // We're not covering the hovering and pressing states here, as they are interactive states that are triggered
        // by mouse or keyboard input. Also, returning null here is fine, as the CodepointToSymbolConverter will handle
        // the null value and return a DependencyProperty.UnsetValue, which will be ignored by the converter.
        return symbol ?? IconAssist.GetIcon(element) ?? IconAssist.GetIconOnSelecting(element);
    }
}