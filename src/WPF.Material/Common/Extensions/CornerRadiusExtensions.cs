namespace WPF.Material.Common;

internal static class CornerRadiusExtensions
{
    public static CornerRadius AddScalar(this CornerRadius radius, double value)
    {
        return new CornerRadius(
            radius.TopLeft + value,
            radius.TopRight + value,
            radius.BottomRight + value,
            radius.BottomLeft + value);
    }
}