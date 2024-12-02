using System.Windows.Markup;

namespace WPF.Material.Common;

[MarkupExtensionReturnType(typeof(CodepointToStringConverter))]
internal sealed class CodepointToStringConverter : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider services) => this;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is null
            ? DependencyProperty.UnsetValue
            : char.ConvertFromUtf32((int)value);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}