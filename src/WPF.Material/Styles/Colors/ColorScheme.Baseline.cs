namespace WPF.Material.Styles;

public readonly partial record struct ColorScheme
{
    /// <summary>
    /// Gets the baseline light normal-contrasted color scheme.
    /// </summary>
    /// <returns>The light normal-contrasted color scheme.</returns>
    public static ColorScheme Light() => new(Brightness.Light, Contrast.Normal)
    {
        Primary = 0x65558F.ToColor(),
        OnPrimary = 0xFFFFFF.ToColor(),
        PrimaryContainer = 0xEADDFF.ToColor(),
        OnPrimaryContainer = 0x4F378A.ToColor(),
        PrimaryFixed = 0xEADDFF.ToColor(),
        OnPrimaryFixed = 0x21005D.ToColor(),
        PrimaryFixedDim = 0xD0BCFF.ToColor(),
        OnPrimaryFixedVariant = 0x4F378B.ToColor(),
        Secondary = 0x625B71.ToColor(),
        OnSecondary = 0xFFFFFF.ToColor(),
        SecondaryContainer = 0xE8DEF8.ToColor(),
        OnSecondaryContainer = 0x4A4459.ToColor(),
        SecondaryFixed = 0xE8DEF8.ToColor(),
        OnSecondaryFixed = 0x1D192B.ToColor(),
        SecondaryFixedDim = 0xCCC2DC.ToColor(),
        OnSecondaryFixedVariant = 0x4A4458.ToColor(),
        Tertiary = 0x7D5260.ToColor(),
        OnTertiary = 0xFFFFFF.ToColor(),
        TertiaryContainer = 0xFFD8E4.ToColor(),
        OnTertiaryContainer = 0x633B48.ToColor(),
        TertiaryFixed = 0xFFD8E4.ToColor(),
        OnTertiaryFixed = 0x31111D.ToColor(),
        TertiaryFixedDim = 0xEFB8C8.ToColor(),
        OnTertiaryFixedVariant = 0x633B48.ToColor(),
        Error = 0xB3261E.ToColor(),
        OnError = 0xFFFFFF.ToColor(),
        ErrorContainer = 0xF9DEDC.ToColor(),
        OnErrorContainer = 0x852221.ToColor(),
        Surface = 0xFEF7FF.ToColor(),
        OnSurface = 0x1D1B20.ToColor(),
        OnSurfaceVariant = 0x49454F.ToColor(),
        SurfaceDim = 0xDED8E1.ToColor(),
        SurfaceBright = 0xFEF7FF.ToColor(),
        SurfaceContainerHighest = 0xE6E0E9.ToColor(),
        SurfaceContainerHigh = 0xECE6F0.ToColor(),
        SurfaceContainer = 0xF3EDF7.ToColor(),
        SurfaceContainerLow = 0xF7F2FA.ToColor(),
        SurfaceContainerLowest = 0xFFFFFF.ToColor(),
        InverseSurface = 0x322F35.ToColor(),
        InverseOnSurface = 0xF5EFF7.ToColor(),
        InversePrimary = 0xD0BCFF.ToColor(),
        Outline = 0x79747E.ToColor(),
        OutlineVariant = 0xCAC4D0.ToColor(),
        Scrim = 0x000000.ToColor()
    };

    /// <summary>
    /// Gets the baseline dark normal-contrasted color scheme.
    /// </summary>
    /// <returns>The dark normal-contrasted color scheme.</returns>
    public static ColorScheme Dark() => new(Brightness.Dark, Contrast.Normal)
    {
        Primary = 0xD0BCFE.ToColor(),
        OnPrimary = 0x381E72.ToColor(),
        PrimaryContainer = 0x4F378B.ToColor(),
        OnPrimaryContainer = 0xEADDFF.ToColor(),
        PrimaryFixed = 0xEADDFF.ToColor(),
        OnPrimaryFixed = 0x21005D.ToColor(),
        PrimaryFixedDim = 0xD0BCFF.ToColor(),
        OnPrimaryFixedVariant = 0x4F378B.ToColor(),
        Secondary = 0xCCC2DC.ToColor(),
        OnSecondary = 0x332D41.ToColor(),
        SecondaryContainer = 0x4A4458.ToColor(),
        OnSecondaryContainer = 0xE8DEF8.ToColor(),
        SecondaryFixed = 0xE8DEF8.ToColor(),
        OnSecondaryFixed = 0x1D192B.ToColor(),
        SecondaryFixedDim = 0xCCC2DC.ToColor(),
        OnSecondaryFixedVariant = 0x4A4458.ToColor(),
        Tertiary = 0xEFB8C8.ToColor(),
        OnTertiary = 0x492532.ToColor(),
        TertiaryContainer = 0x633B48.ToColor(),
        OnTertiaryContainer = 0xFFD8E4.ToColor(),
        TertiaryFixed = 0xFFD8E4.ToColor(),
        OnTertiaryFixed = 0x31111D.ToColor(),
        TertiaryFixedDim = 0xEFB8C8.ToColor(),
        OnTertiaryFixedVariant = 0x633B48.ToColor(),
        Error = 0xF2B8B5.ToColor(),
        OnError = 0x601410.ToColor(),
        ErrorContainer = 0x8C1D18.ToColor(),
        OnErrorContainer = 0xF9DEDC.ToColor(),
        Surface = 0x141218.ToColor(),
        OnSurface = 0xE6E0E9.ToColor(),
        OnSurfaceVariant = 0xCAC4D0.ToColor(),
        SurfaceDim = 0x141218.ToColor(),
        SurfaceBright = 0x3B383E.ToColor(),
        SurfaceContainerHighest = 0x36343B.ToColor(),
        SurfaceContainerHigh = 0x2B2930.ToColor(),
        SurfaceContainer = 0x211F26.ToColor(),
        SurfaceContainerLow = 0x1D1B20.ToColor(),
        SurfaceContainerLowest = 0x0F0D13.ToColor(),
        InverseSurface = 0xE2E2E9.ToColor(),
        InverseOnSurface = 0x2E3036.ToColor(),
        InversePrimary = 0x415F91.ToColor(),
        Outline = 0x938F99.ToColor(),
        OutlineVariant = 0x49454F.ToColor(),
        Scrim = 0x000000.ToColor()
    };
}