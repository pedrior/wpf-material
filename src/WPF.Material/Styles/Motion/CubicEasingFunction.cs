using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media.Animation;

namespace WPF.Material.Styles;

internal class CubicEasingFunction : IEasingFunction
{
    public CubicEasingFunction(double x1, double y1, double x2, double y2)
    {
        ValidateArgument(x1);
        ValidateArgument(y1);
        ValidateArgument(x2);
        ValidateArgument(y2);

        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }

    protected double X1 { get; }

    protected double Y1 { get; }

    protected double X2 { get; }

    protected double Y2 { get; }

    public double Ease(double t) => Evaluate(t);

    protected virtual double Evaluate(double t) => CubicEvaluator.Evaluate(X1, Y1, X2, Y2, t);

    [DebuggerStepThrough]
    protected static void ValidateArgument(
        double value,
        [CallerArgumentExpression("value")] string? argumentName = null)
    {
        if (value is < 0.0 or > 1.0)
        {
            throw new ArgumentOutOfRangeException(argumentName, value, "Value must be between 0.0 and 1.0.");
        }
    }
}