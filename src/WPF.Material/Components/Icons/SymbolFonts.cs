namespace WPF.Material.Components;

internal static class SymbolFonts
{
    private const string UriFormat = "pack://application:,,,/WPF.Material.Symbols.{0};component/Assets/";
    private const string FontFamilyFormat = "./#Material Symbols {0}{1}";
    
    private static readonly FontFamily RoundedFont;
    private static readonly FontFamily RoundedFilledFont;
    private static readonly FontFamily SharpFont;
    private static readonly FontFamily SharpFilledFont;
    private static readonly FontFamily OutlinedFont;
    private static readonly FontFamily OutlinedFilledFont;

    static SymbolFonts()
    {
        RoundedFont = new FontFamily(
            new Uri(string.Format(UriFormat, "Rounded")),
            string.Format(FontFamilyFormat, "Rounded", string.Empty));

        RoundedFilledFont = new FontFamily(
            new Uri(string.Format(UriFormat, "Rounded")),
            string.Format(FontFamilyFormat, "Rounded", " Filled"));

        SharpFont = new FontFamily(
            new Uri(string.Format(UriFormat, "Sharp")),
            string.Format(FontFamilyFormat, "Sharp", string.Empty));

        SharpFilledFont = new FontFamily(
            new Uri(string.Format(UriFormat, "Sharp")),
            string.Format(FontFamilyFormat, "Sharp", " Filled"));

        OutlinedFont = new FontFamily(
            new Uri(string.Format(UriFormat, "Outlined")),
            string.Format(FontFamilyFormat, "Outlined", string.Empty));

        OutlinedFilledFont = new FontFamily(
            new Uri(string.Format(UriFormat, "Outlined")),
            string.Format(FontFamilyFormat, "Outlined", " Filled"));
    }

    public static FontFamily GetSymbolFont(SymbolType type, bool isFilled) => type switch
    {
        SymbolType.Rounded => isFilled ? RoundedFilledFont : RoundedFont,
        SymbolType.Sharp => isFilled ? SharpFilledFont : SharpFont,
        SymbolType.Outlined => isFilled ? OutlinedFilledFont : OutlinedFont,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };
}