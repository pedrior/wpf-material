using System.Collections.Frozen;
using static System.Windows.FontWeights;

namespace WPF.Material.Styles;

internal static class TypeScale
{
    private static readonly FrozenDictionary<TypeStyle, FontStyle> Default =
        new Dictionary<TypeStyle, FontStyle>
        {
            [TypeStyle.DisplayLarge] = new(Typefaces.Default, FontSize: 57, LineHeight: 64, Normal),
            [TypeStyle.DisplayMedium] = new(Typefaces.Default, FontSize: 45, LineHeight: 52, Normal),
            [TypeStyle.DisplaySmall] = new(Typefaces.Default, FontSize: 36, LineHeight: 44, Normal),
            [TypeStyle.HeadlineLarge] = new(Typefaces.Default, FontSize: 32, LineHeight: 40, Normal),
            [TypeStyle.HeadlineMedium] = new(Typefaces.Default, FontSize: 28, LineHeight: 36, Normal),
            [TypeStyle.HeadlineSmall] = new(Typefaces.Default, FontSize: 24, LineHeight: 32, Normal),
            [TypeStyle.TitleLarge] = new(Typefaces.Default, FontSize: 22, LineHeight: 28, Normal),
            [TypeStyle.TitleMedium] = new(Typefaces.Default, FontSize: 16, LineHeight: 24, Normal),
            [TypeStyle.TitleSmall] = new(Typefaces.Default, FontSize: 14, LineHeight: 20, Medium),
            [TypeStyle.BodyLarge] = new(Typefaces.Default, FontSize: 16, LineHeight: 24, Normal),
            [TypeStyle.BodyMedium] = new(Typefaces.Default, FontSize: 14, LineHeight: 20, Normal),
            [TypeStyle.BodySmall] = new(Typefaces.Default, FontSize: 12, LineHeight: 16, Normal),
            [TypeStyle.LabelLarge] = new(Typefaces.Default, FontSize: 14, LineHeight: 20, Medium),
            [TypeStyle.LabelMedium] = new(Typefaces.Default, FontSize: 12, LineHeight: 16, Medium),
            [TypeStyle.LabelSmall] = new(Typefaces.Default, FontSize: 11, LineHeight: 16, Medium)
        }.ToFrozenDictionary();

    public static FontStyle GetFontStyle(TypeStyle style) => Default[style];
}