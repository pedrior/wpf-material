namespace WPF.Material.Common;

internal static class ThicknessExtensions
{
    public static Thickness Add(this Thickness first, Thickness second) => new(
        first.Left + second.Left,
        first.Top + second.Top,
        first.Right + second.Right,
        first.Bottom + second.Bottom);
}