using System.Windows.Markup;

namespace WPF.Material.Converters;

[ValueConversion(typeof(Thickness), typeof(double))]
internal sealed class ThicknessToDoubleConverter : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider services) => this;
    
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is Thickness thickness 
            ? thickness.Left 
            : DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => 
        throw new NotSupportedException();
}