namespace WPF.Material.Theming;

/// <summary>
/// Represents a color scheme for the Material Design 3 theme.
/// </summary>
/// <param name="Brightness">The brightness which the color scheme is based on.</param>
/// <param name="Contrast">The contrast which the color scheme is based on.</param>
public readonly partial record struct ColorScheme(Brightness Brightness, Contrast Contrast)
{
    /// <summary>
    /// Gets the brightness which the color scheme is based on.
    /// </summary>
    public Brightness Brightness { get; } = Brightness;

    /// <summary>
    /// Gets the contrast which the color scheme is based on.
    /// </summary>
    public Contrast Contrast { get; } = Contrast;

    /// <summary>
    /// Gets the primary color.
    /// </summary>
    public Color Primary { get; init; }

    /// <summary>
    /// Gets the on-primary color.
    /// </summary>
    public Color OnPrimary { get; init; }

    /// <summary>
    /// Gets the primary container color.
    /// </summary>
    public Color PrimaryContainer { get; init; }

    /// <summary>
    /// Gets the on-primary container color.
    /// </summary>
    public Color OnPrimaryContainer { get; init; }

    /// <summary>
    /// Gets the primary fixed color.
    /// </summary>
    public Color PrimaryFixed { get; init; }

    /// <summary>
    /// Gets the on-primary fixed color.
    /// </summary>
    public Color OnPrimaryFixed { get; init; }

    /// <summary>
    /// Gets the primary fixed dim color.
    /// </summary>
    public Color PrimaryFixedDim { get; init; }

    /// <summary>
    /// Gets the primary fixed variant color.
    /// </summary>
    public Color OnPrimaryFixedVariant { get; init; }

    /// <summary>
    /// Gets the secondary color.
    /// </summary>
    public Color Secondary { get; init; }

    /// <summary>
    /// Gets the on-secondary color.
    /// </summary>
    public Color OnSecondary { get; init; }

    /// <summary>
    /// Gets the secondary container color.
    /// </summary>
    public Color SecondaryContainer { get; init; }

    /// <summary>
    /// Gets the on-secondary container color.
    /// </summary>
    public Color OnSecondaryContainer { get; init; }

    /// <summary>
    /// Gets the secondary fixed color.
    /// </summary>
    public Color SecondaryFixed { get; init; }

    /// <summary>
    /// Gets the on-secondary fixed color.
    /// </summary>
    public Color OnSecondaryFixed { get; init; }

    /// <summary>
    /// Gets the secondary fixed dim color.
    /// </summary>
    public Color SecondaryFixedDim { get; init; }

    /// <summary>
    /// Gets the secondary fixed variant color.
    /// </summary>
    public Color OnSecondaryFixedVariant { get; init; }

    /// <summary>
    /// Gets the tertiary color.
    /// </summary>
    public Color Tertiary { get; init; }

    /// <summary>
    /// Gets the on-tertiary color.
    /// </summary>
    public Color OnTertiary { get; init; }

    /// <summary>
    /// Gets the tertiary container color.
    /// </summary>
    public Color TertiaryContainer { get; init; }

    /// <summary>
    /// Gets the on-tertiary container color.
    /// </summary>
    public Color OnTertiaryContainer { get; init; }

    /// <summary>
    /// Gets the tertiary fixed color.
    /// </summary>
    public Color TertiaryFixed { get; init; }

    /// <summary>
    /// Gets the on-tertiary fixed color.
    /// </summary>
    public Color OnTertiaryFixed { get; init; }

    /// <summary>
    /// Gets the tertiary fixed dim color.
    /// </summary>
    public Color TertiaryFixedDim { get; init; }

    /// <summary>
    /// Gets the tertiary fixed variant color.
    /// </summary>
    public Color OnTertiaryFixedVariant { get; init; }

    /// <summary>
    /// Gets the error color.
    /// </summary>
    public Color Error { get; init; }

    /// <summary>
    /// Gets the on-error color.
    /// </summary>
    public Color OnError { get; init; }

    /// <summary>
    /// Gets the error container color.
    /// </summary>
    public Color ErrorContainer { get; init; }

    /// <summary>
    /// Gets the on-error container color.
    /// </summary>
    public Color OnErrorContainer { get; init; }

    /// <summary>
    /// Gets the surface color.
    /// </summary>
    public Color Surface { get; init; }

    /// <summary>
    /// Gets the on-surface color.
    /// </summary>
    public Color OnSurface { get; init; }

    /// <summary>
    /// Gets the on-surface variant color.
    /// </summary>
    public Color OnSurfaceVariant { get; init; }

    /// <summary>
    /// Gets the surface dim color.
    /// </summary>
    public Color SurfaceDim { get; init; }

    /// <summary>
    /// Gets the surface bright color.
    /// </summary>
    public Color SurfaceBright { get; init; }

    /// <summary>
    /// Gets the surface container highest color.
    /// </summary>
    public Color SurfaceContainerHighest { get; init; }

    /// <summary>
    /// Gets the surface container high color.
    /// </summary>
    public Color SurfaceContainerHigh { get; init; }

    /// <summary>
    /// Gets the surface container color.
    /// </summary>
    public Color SurfaceContainer { get; init; }

    /// <summary>
    /// Gets the surface container low color.
    /// </summary>
    public Color SurfaceContainerLow { get; init; }

    /// <summary>
    /// Gets the surface container lowest color.
    /// </summary>
    public Color SurfaceContainerLowest { get; init; }

    /// <summary>
    /// Gets the inverse surface color.
    /// </summary>
    public Color InverseSurface { get; init; }

    /// <summary>
    /// Gets the inverse on-surface color.
    /// </summary>
    public Color InverseOnSurface { get; init; }

    /// <summary>
    /// Gets the inverse primary color.
    /// </summary>
    public Color InversePrimary { get; init; }

    /// <summary>
    /// Gets the outline color.
    /// </summary>
    public Color Outline { get; init; }

    /// <summary>
    /// Gets the outline variant color.
    /// </summary>
    public Color OutlineVariant { get; init; }
    
    /// <summary>
    /// Gets the scrim color.
    /// </summary>
    public Color Scrim { get; init; }
}