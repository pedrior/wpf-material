namespace WPF.Material.Styles;

internal static class ShapeGeometry
{
    public static StreamGeometry Create(
        ShapeFamily family,
        Rect bounds,
        CornerRadius radius,
        bool isFilled,
        bool isStroked)
    {
        var geometry = new StreamGeometry();
        using (var context = geometry.Open())
        {
            if (family is ShapeFamily.Rounded)
            {
                CreateRectangle(context, bounds, radius, isFilled, isStroked);
            }
            else
            {
                CreateOctagon(context, bounds, radius, isFilled, isStroked);
            }
        }

        geometry.Freeze();
        return geometry;
    }

    private static void CreateRectangle(
        StreamGeometryContext context,
        Rect bounds,
        CornerRadius radius,
        bool isFilled,
        bool isStroked)
    {
        var p0 = new Point(radius.TopLeft + bounds.X, bounds.Y);

        var p1 = new Point(bounds.Width + bounds.X - radius.TopRight, bounds.Y);
        var p2 = new Point(bounds.Width + bounds.X, bounds.Height + bounds.Y - radius.BottomLeft);
        var p3 = new Point(radius.BottomRight + bounds.X, bounds.Height + bounds.Y);
        var p4 = new Point(bounds.X, radius.TopLeft + bounds.Y);

        var a1 = new Point(bounds.Width + bounds.X, radius.TopRight + bounds.Y);
        var a2 = new Point(bounds.Width + bounds.X - radius.BottomLeft, bounds.Height + bounds.Y);
        var a3 = new Point(bounds.X, bounds.Height + bounds.Y - radius.BottomRight);
        var a4 = new Point(radius.TopLeft + bounds.X, bounds.Y);

        context.BeginFigure(startPoint: p0, isFilled, isClosed: true);

        LineTo(context, p1, isStroked);
        ArcTo(context, a1, radius.TopRight, isStroked);

        LineTo(context, p2, isStroked);
        ArcTo(context, a2, radius.BottomLeft, isStroked);

        LineTo(context, p3, isStroked);
        ArcTo(context, a3, radius.BottomRight, isStroked);

        LineTo(context, p4, isStroked);
        ArcTo(context, a4, radius.TopLeft, isStroked);
    }

    private static void CreateOctagon(
        StreamGeometryContext context,
        Rect bounds,
        CornerRadius radius,
        bool isFilled,
        bool isStroked)
    {
        var p0 = new Point(radius.TopLeft + bounds.X, bounds.Y);

        var p1 = new Point(bounds.Width + bounds.X - radius.TopRight, bounds.Y);
        var p2 = new Point(bounds.Width + bounds.X, radius.TopRight + bounds.Y);
        var p3 = new Point(bounds.Width + bounds.X, bounds.Height - radius.BottomLeft + bounds.Y);
        var p4 = new Point(bounds.Width - radius.BottomLeft + bounds.X, bounds.Height + bounds.Y);
        var p5 = new Point(radius.BottomRight + bounds.X, bounds.Height + bounds.Y);
        var p6 = new Point(bounds.X, bounds.Height - radius.BottomRight + bounds.Y);
        var p7 = new Point(bounds.X, radius.TopLeft + bounds.Y);

        context.BeginFigure(p0, isFilled, isClosed: true);

        LineTo(context, p1, isStroked);
        LineTo(context, p2, isStroked);
        LineTo(context, p3, isStroked);
        LineTo(context, p4, isStroked);
        LineTo(context, p5, isStroked);
        LineTo(context, p6, isStroked);
        LineTo(context, p7, isStroked);
    }

    private static void LineTo(StreamGeometryContext context, Point point, bool isStroked) =>
        context.LineTo(point, isStroked, isSmoothJoin: false);

    private static void ArcTo(StreamGeometryContext context, Point point, double size, bool isStroked)
    {
        if (size > 0.0)
        {
            context.ArcTo(
                point,
                new Size(size, size),
                rotationAngle: 0.0,
                isLargeArc: false,
                sweepDirection: SweepDirection.Clockwise,
                isStroked,
                isSmoothJoin: false);
        }
    }
}