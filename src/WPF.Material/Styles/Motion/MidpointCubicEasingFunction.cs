namespace WPF.Material.Styles;

internal class MidpointCubicEasingFunction : CubicEasingFunction
{
    public MidpointCubicEasingFunction(
        double x1,
        double y1,
        double x2,
        double y2,
        double x3,
        double y3,
        double x4,
        double y4,
        double mx,
        double my)
        : base(x1, y1, x2, y2)
    {
        ValidateArgument(x3);
        ValidateArgument(y3);
        ValidateArgument(x4);
        ValidateArgument(y4);
        ValidateArgument(mx);
        ValidateArgument(my);

        X3 = x3;
        Y3 = y3;
        X4 = x4;
        Y4 = y4;
        Mx = mx;
        My = my;
    }

    protected double X3 { get; }

    protected double Y3 { get; }

    protected double X4 { get; }

    protected double Y4 { get; }

    protected double Mx { get; }

    protected double My { get; }

    protected override double Evaluate(double t)
    {
        if (Mx is 0.0 && My is 0.0)
        {
            return base.Evaluate(t);
        }

        var firstCurve = t < Mx;

        var scaleX = firstCurve ? Mx : 1.0 - Mx;
        var scaleY = firstCurve ? My : 1.0 - My;
        var scaledT = (t - (firstCurve ? 0.0 : Mx)) / scaleX;

        if (firstCurve)
        {
            return CubicEvaluator.Evaluate(
                X1 / scaleX,
                Y1 / scaleY,
                X2 / scaleX,
                Y2 / scaleY,
                scaledT) * scaleY;
        }

        return CubicEvaluator.Evaluate(
            (X3 - Mx) / scaleX,
            (Y3 - My) / scaleY,
            (X4 - Mx) / scaleX,
            (Y4 - My) / scaleY,
            scaledT) * scaleY + My;
    }
}