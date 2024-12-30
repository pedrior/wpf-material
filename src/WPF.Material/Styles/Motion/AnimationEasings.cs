using System.Windows.Media.Animation;

namespace WPF.Material.Styles;

// Reference: https://m3.material.io/styles/motion/easing-and-duration/tokens-specs

/// <summary>
/// Contains a set of predefined curve functions for animations.
/// </summary>
public static class AnimationEasings
{
    /// <summary>
    /// Represents the curve function used to create an emphasized animation.
    /// </summary>
    public static readonly Curve Emphasized = new ThreePointCubicCurve(
        a1: new Point(0.05, 0.0),
        b1: new Point(0.133333, 0.06),
        mp: new Point(0.166666, 0.4),
        a2: new Point(0.2083333, 0.82),
        b2: new Point(0.25, 1));

    /// <summary>
    /// Represents the curve function used to create an emphasized accelerated animation.
    /// </summary>
    public static readonly IEasingFunction EmphasizedAccelerated = new CubicCurve(
        a: new Point(0.3, 0.0),
        b: new Point(0.8, 0.15));

    public static readonly IEasingFunction EmphasizedDecelerated = new CubicCurve(
        a: new Point(0.5, 0.7),
        b: new Point(0.1, 1.0));

    /// <summary>
    /// Represents the curve function used to create a standard animation.
    /// </summary>
    public static readonly IEasingFunction Standard = new CubicCurve(
        a: new Point(0.2, 0.0),
        b: new Point(0.0, 1.0));

    /// <summary>
    /// Represents the curve function used to create a standard accelerated animation.
    /// </summary>
    public static readonly IEasingFunction StandardAccelerated = new CubicCurve(
        a: new Point(0.3, 0.0),
        b: new Point(1.0, 1.0));

    /// <summary>
    /// Represents the curve function used to create a standard decelerated animation.
    /// </summary>
    public static readonly IEasingFunction StandardDecelerated = new CubicCurve(
        a: new Point(0.0, 0.0),
        b: new Point(0.0, 1.0));
}