using System.Runtime.CompilerServices;

namespace WPF.Material.Styles;

internal static class CubicEvaluator
{
    private const double cubicErrorBound = 0.001;

    public static double Evaluate(double x1, double y1, double x2, double y2, double t)
    {
        var start = 0.0;
        var end = 1.0;

        while (true)
        {
            var midpoint = (start + end) * 0.5;
            var estimate = GetCubicBezierValueAtT(x1, x2, midpoint);

            if (Math.Abs(t - estimate) < cubicErrorBound)
            {
                return GetCubicBezierValueAtT(y1, y2, midpoint);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double GetCubicBezierValueAtT(double a, double b, double t) =>
        3 * a * (1 - t) * (1 - t) * t + 3 * b * (1 - t) * t * t + t * t * t;
}