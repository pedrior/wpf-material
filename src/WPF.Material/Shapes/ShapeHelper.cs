using System.Runtime.CompilerServices;

namespace WPF.Material.Shapes;

internal static class ShapeHelper
{
    private const double NoneRadius = 0.0;
    private const double ExtraSmallRadius = 4.0;
    private const double SmallRadius = 8.0;
    private const double MediumRadius = 12.0;
    private const double LargeRadius = 16.0;
    private const double ExtraLargeRadius = 28.0;

    public static CornerRadius ClampCornerRadius(
        CornerRadius radius,
        ShapeStyle style,
        double width,
        double height,
        bool useStyleRadius = true,
        ShapeCorner cornersOverride = ShapeCorner.All)
    {
        var maxRadius = Math.Min(width, height) * 0.5;
        var styleRadius = useStyleRadius
            ? GetRadiusForStyle(style, maxRadius)
            : 0.0;

        var tl = ClampRadius(radius.TopLeft, ShapeCorner.TopLeft);
        var tr = ClampRadius(radius.TopRight, ShapeCorner.TopRight);
        var bl = ClampRadius(radius.BottomLeft, ShapeCorner.BottomLeft);
        var br = ClampRadius(radius.BottomRight, ShapeCorner.BottomRight);

        return new CornerRadius(tl, tr, bl, br);

        double ClampRadius(double cornerRadius, ShapeCorner shapeCorner)
        {
            if (IsCornerSet(cornersOverride, shapeCorner))
            {
                return Math.Clamp(cornerRadius, 0.0, maxRadius);
            }
            
            return useStyleRadius 
                ? styleRadius 
                : Math.Clamp(cornerRadius, 0.0, maxRadius);
        }
    }

    public static CornerRadius GetRadiusForStyle(ShapeStyle style, ShapeCorner corner, double width, double height)
    {
        var maxRadius = Math.Min(width, height) * 0.5;

        var tl = IsCornerSet(corner, ShapeCorner.TopLeft) ? GetRadiusForStyle(style, maxRadius) : 0.0;
        var tr = IsCornerSet(corner, ShapeCorner.TopRight) ? GetRadiusForStyle(style, maxRadius) : 0.0;
        var bl = IsCornerSet(corner, ShapeCorner.BottomLeft) ? GetRadiusForStyle(style, maxRadius) : 0.0;
        var br = IsCornerSet(corner, ShapeCorner.BottomRight) ? GetRadiusForStyle(style, maxRadius) : 0.0;

        return new CornerRadius(tl, tr, bl, br);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsCornerSet(ShapeCorner value, ShapeCorner flag) => (value & flag) is not 0;

    private static double GetRadiusForStyle(ShapeStyle style, double maxRadius)
    {
        var radius = style switch
        {
            ShapeStyle.None => NoneRadius,
            ShapeStyle.ExtraSmall => ExtraSmallRadius,
            ShapeStyle.Small => SmallRadius,
            ShapeStyle.Medium => MediumRadius,
            ShapeStyle.Large => LargeRadius,
            ShapeStyle.ExtraLarge => ExtraLargeRadius,
            ShapeStyle.Full => maxRadius,
            _ => throw new ArgumentOutOfRangeException(nameof(style), style, null)
        };

        return Math.Clamp(radius, 0.0, maxRadius);
    }
}