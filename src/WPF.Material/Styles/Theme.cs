namespace WPF.Material.Styles;

/// <summary>
/// Represents a Material Design 3 theme, which defines the color scheme and typeface of the application. You can use
/// one of the predefined themes, such as <see cref="Light"/> or <see cref="Dark"/>. If you need more customization,
/// you can create a new one and define your own typeface and colors.
/// </summary>
public readonly record struct Theme
{
    private static readonly Lazy<Theme> LazyLight = new(() => new Theme());

    private static readonly Lazy<Theme> LazyDark = new(
        () => new Theme(Typefaces.Default, ColorScheme.Dark()));

    /// <summary>
    /// Creates a new instance of the <see cref="Theme"/> class with the default light color scheme and Default typeface.
    /// </summary>
    public Theme()
    {
        Typeface = Typefaces.Default;
        Colors = ColorScheme.Light();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Theme"/> class with the specified color scheme and typeface.
    /// </summary>
    /// <param name="typeface">The default typeface name of the theme.</param>
    /// <param name="colors">The color scheme of the theme.</param>
    public Theme(string typeface, ColorScheme colors)
    {
        Typeface = typeface;
        Colors = colors;
    }

    /// <summary>
    /// Gets the default normal-contrasted light theme.
    /// </summary>
    public static Theme Light => LazyLight.Value;

    /// <summary>
    /// Gets the default normal-contrasted dark theme.
    /// </summary>
    public static Theme Dark => LazyDark.Value;

    /// <summary>
    /// Gets the color scheme of the theme.
    /// </summary>
    public ColorScheme Colors { get; init; }

    /// <summary>
    /// Gets the default typeface name of the theme.
    /// </summary>
    public string Typeface { get; init; }

    /// <summary>
    /// Get the brightness of the theme. This is determined by the color scheme.
    /// </summary>
    public Brightness Brightness => Colors.Brightness;
}