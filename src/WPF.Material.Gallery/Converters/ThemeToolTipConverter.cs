using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPF.Material.Gallery.Converters;

internal sealed class ThemeToolTipConverter : MarkupExtension, IValueConverter
{
    public object? OnLightTooltip { get; set; }

    public object? OnDarkTooltip { get; set; }

    public override object ProvideValue(IServiceProvider services) => this;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var isDarkTheme = value is true;
        return isDarkTheme ? OnDarkTooltip : OnLightTooltip;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}