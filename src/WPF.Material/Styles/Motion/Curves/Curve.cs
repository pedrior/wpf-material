using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media.Animation;

namespace WPF.Material.Styles;

/// <summary>
/// Provides a base class for creating custom easing functions.
/// </summary>
public abstract class Curve : IEasingFunction
{
    public abstract double Ease(double t);

    [DebuggerStepThrough]
    protected static void ValidatePoint(Point point, [CallerArgumentExpression("point")] string? argument = null)
    {
        if (point is { X: < 0.0 or > 1.0 } or { Y: < 0.0 or > 1.0 })
        {
            throw new ArgumentException($"Expected a point in the range [0, 1], but got {point} for {argument}.");
        }
    }
}