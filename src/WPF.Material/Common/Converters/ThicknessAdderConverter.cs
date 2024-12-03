using System.Windows.Markup;

namespace WPF.Material.Common;

[MarkupExtensionReturnType(typeof(ThicknessAdderConverter))]
internal sealed class ThicknessAdderConverter : MarkupExtension, IMultiValueConverter
{
    public override object ProvideValue(IServiceProvider provider) => this;

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return values
            .Cast<Thickness>()
            .Aggregate(new Thickness(), (current, thickness) => current.Add(thickness));
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}