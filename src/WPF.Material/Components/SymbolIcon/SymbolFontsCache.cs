using System.Collections.Concurrent;

namespace WPF.Material.Components;

internal static class SymbolFontsCache
{
    private const string SymbolFontFamilyFormat = "Material Symbols {0}{1}";
    private const string SymbolFontSourceFormat = "pack://application:,,,/WPF.Material.Symbols.{0};component/Assets/";

    private static readonly ConcurrentDictionary<(SymbolType, bool), FontFamily> Cache = new();

    public static FontFamily GetOrLoad(SymbolType symbolType, bool isFilled)
    {
        if (Cache.TryGetValue((symbolType, isFilled), out var fontFamily))
        {
            return fontFamily;
        }

        var familyName = string.Format(SymbolFontFamilyFormat, symbolType, isFilled ? " Filled" : string.Empty);
        var fontSource = new Uri(string.Format(SymbolFontSourceFormat, symbolType));

        fontFamily = new FontFamily(fontSource, $"./#{familyName}");

        Cache.TryAdd((symbolType, isFilled), fontFamily);
        
        return fontFamily;
    }
}