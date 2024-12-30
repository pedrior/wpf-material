namespace WPF.Material.Styles;

// Reference: https://github.com/flutter/flutter/blob/0ffc4ce00ea7bb912e379adf39354644eab2c17e/packages/flutter/lib/src/animation/curves.dart

/// <summary>
/// Implements a Bézier curve function.
/// </summary>
public sealed class CubicCurve : Curve
{
    private const double errorBound = 0.001;

    private readonly Point a;
    private readonly Point b;

    /// <summary>
    /// Initializes a new instance of <see cref="CubicCurve"/>.
    /// </summary>
    /// <param name="a">The first control point.</param>
    /// <param name="b">The second control point.</param>
    public CubicCurve(Point a, Point b)
    {
        ValidatePoint(a);
        ValidatePoint(b);

        this.a = a;
        this.b = b;
    }

    public override double Ease(double t) => Transform(a.X, a.Y, b.X, b.Y, t);

    public override string ToString() => $"{nameof(CubicCurve)}({a.X}, {a.Y}, {b.X}, {b.Y})";

    internal static double Transform(double ax, double ay, double bx, double by, double t)
    {
        var start = 0.0;
        var end = 1.0;

        while (true)
        {
            var midpoint = (start + end) * 0.5;
            var estimate = Transform(ax, bx, midpoint);

            if (Math.Abs(t - estimate) < errorBound)
            {
                return Transform(ay, by, midpoint);
            }

            if (estimate < t)
            {
                start = midpoint;
            }
            else
            {
                end = midpoint;
            }
        }
    }

    private static double Transform(double a, double b, double t) =>
        3 * a * (1 - t) * (1 - t) * t + 3 * b * (1 - t) * t * t + t * t * t;
}