namespace WPF.Material.Components;

internal static class SymbolRender
{
    private const ushort NotDefGlyph = 0;

    private static readonly Dictionary<(SymbolType, FontWeight, bool), GlyphTypeface> GlyphTypefaces = new(14);

    public static void RenderSymbolAsGlyph(
        DrawingContext context,
        double width,
        double height,
        Symbol symbol,
        SymbolType type,
        FontWeight weight,
        Brush brush,
        bool isFilled,
        double ppd)
    {
        var typeface = GetGlyphTypeface(type, weight, isFilled);
        var glyphIndex = GetSymbolGlyphIndex(typeface, (int)symbol);

        var size = Math.Min(width, height);

        // Center the glyph in the available space.
        var origin = new Point(
            x: (width - size) * 0.5,
            y: (height + size) * 0.5);

        var glyph = new GlyphRun(
            glyphTypeface: typeface,
            bidiLevel: 0,
            isSideways: false,
            renderingEmSize: size,
            pixelsPerDip: (float)ppd,
            glyphIndices: new[] { glyphIndex },
            baselineOrigin: origin,
            advanceWidths: new[] { size },
            glyphOffsets: null,
            characters: null,
            deviceFontName: null,
            clusterMap: null,
            caretStops: null,
            language: null);

        context.DrawGlyphRun(brush, glyph);
    }

    private static GlyphTypeface GetGlyphTypeface(SymbolType type, FontWeight weight, bool isFilled)
    {
        var key = (type, weight, isFilled);
        if (GlyphTypefaces.TryGetValue(key, out var cachedTypeface))
        {
            return cachedTypeface;
        }

        var font = SymbolFonts.GetSymbolFont(type, isFilled);
        var typeface = new Typeface(
            font,
            FontStyles.Normal,
            weight,
            FontStretches.Normal);

        if (!typeface.TryGetGlyphTypeface(out var glyphTypeface))
        {
            throw new InvalidOperationException("Unable to get the glyph typeface.");
        }

        GlyphTypefaces[key] = glyphTypeface;
        return glyphTypeface;
    }

    private static ushort GetSymbolGlyphIndex(GlyphTypeface typeface, int glyph) =>
        typeface.CharacterToGlyphMap.TryGetValue(glyph, out var index)
            ? index
            : NotDefGlyph;
}