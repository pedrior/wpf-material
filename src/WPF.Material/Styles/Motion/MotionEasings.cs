using System.Windows.Media.Animation;

namespace WPF.Material.Styles;

/// <summary>
/// Contains a set of predefined easing functions for motion animations.
/// </summary>
public static class MotionEasings
{
    /// <summary>
    /// Represents the emphasized cubic easing function.
    /// </summary>
    public static readonly IEasingFunction Emphasized = new MidpointCubicEasingFunction(
        0.05,
        0,
        0.133333,
        0.06,
        0.2083333,
        0.82,
        0.25,
        1,
        mx: 0.166666,
        my: 0.4);

    /// <summary>
    /// Represents the emphasized accelerated cubic easing function.
    /// </summary>
    public static readonly IEasingFunction EmphasizedAccelerated = new CubicEasingFunction(0.3, 0.0, 0.8, 0.15);

    /// <summary>
    /// Represents the emphasized decelerated cubic easing function.
    /// </summary>
    public static readonly IEasingFunction EmphasizedDecelerated = new CubicEasingFunction(0.5, 0.7, 0.1, 1.0);

    /// <summary>
    /// Represents the standard cubic easing function.
    /// </summary>
    public static readonly IEasingFunction Standard = new CubicEasingFunction(0.2, 0.0, 0.0, 1.0);

    /// <summary>
    /// Represents the standard accelerated cubic easing function.
    /// </summary>
    public static readonly IEasingFunction StandardAccelerated = new CubicEasingFunction(0.3, 0.0, 1.0, 1.0);

    /// <summary>
    /// Represents the standard decelerated cubic easing function.
    /// </summary>
    public static readonly IEasingFunction StandardDecelerated = new CubicEasingFunction(0.0, 0.0, 0.0, 1.0);
}