using System.Windows.Media.Effects;

namespace WPF.Material.Styles;

// Bitmap effects can be resource-intensive, but alternatives are limited. While SystemDropShadowChrome provides a
// low-resource shadow effect by using rectangles and gradients, it has significant limitations compared to the
// DropShadowEffect. In the future, I may consider creating a custom shadow effect based on SystemDropShadowChrome.
// For now, the DropShadowEffect is the best option for creating a shadow effect in WPF.

/// <summary>
/// Contains a set of predefined <see cref="DropShadowEffect"/> representing the elevations levels.
/// </summary>
public static class Elevations
{
    /// <summary>
    /// Represents the level 1 elevation shadow.
    /// </summary>
    public static readonly DropShadowEffect Level1Shadow = CreateShadowEffect(1, 3);

    /// <summary>
    /// Represents the level 2 elevation shadow.
    /// </summary>
    public static readonly DropShadowEffect Level2Shadow = CreateShadowEffect(2, 6);

    /// <summary>
    /// Represents the level 3 elevation shadow.
    /// </summary>
    public static readonly DropShadowEffect Level3Shadow = CreateShadowEffect(4, 8);

    /// <summary>
    /// Represents the level 4 elevation shadow.
    /// </summary>
    public static readonly DropShadowEffect Level4Shadow = CreateShadowEffect(6, 10);

    /// <summary>
    /// Represents the level 5 elevation shadow.
    /// </summary>
    public static readonly DropShadowEffect Level5Shadow = CreateShadowEffect(8, 12);

    private const double Opacity = 0.3;
    private const double Direction = 270;
    private const RenderingBias Bias = RenderingBias.Performance;

    private static readonly Color Color = Colors.Black;

    private static DropShadowEffect CreateShadowEffect(double depth, double blur)
    {
        var effect = new DropShadowEffect
        {
            Color = Color,
            Opacity = Opacity,
            Direction = Direction,
            ShadowDepth = depth,
            BlurRadius = blur,
            RenderingBias = Bias
        };

        effect.Freeze();

        return effect;
    }
}