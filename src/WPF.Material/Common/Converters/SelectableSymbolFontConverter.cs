using System.Windows.Markup;

namespace WPF.Material.Common;

[MarkupExtensionReturnType(typeof(SelectableSymbolFontConverter))]
internal sealed class SelectableSymbolFontConverter : MarkupExtension, IMultiValueConverter
{
    private static readonly Dictionary<(SymbolStyle, bool), FontFamily> Cache = new();

    public override object ProvideValue(IServiceProvider services) => this;

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length is not 2 || values[0] is not SymbolStyle style || values[1] is not bool filled)
        {
            return DependencyProperty.UnsetValue;
        }

        if (Cache.TryGetValue((style, filled), out var font))
        {
            return font;
        }

        font = LoadSymbolFont(style, filled);
        Cache.Add((style, filled), font);

        return font;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();

    private static FontFamily LoadSymbolFont(SymbolStyle style, bool filled)
    {
        var name = $"Material Symbols {style}{(filled ? " Filled" : string.Empty)}";
        var source = new Uri($"pack://application:,,,/WPF.Material.Symbols.{style};component/Assets/");

        return new FontFamily(source, $"./#{name}");
    }
}