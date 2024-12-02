namespace WPF.Material.Styles;

/// <summary>
/// Provides access to the fonts available for the application.
/// </summary>
public static class Typefaces
{
    /// <summary>
    /// Identifies the Roboto typeface. This is the default typeface for the application.
    /// </summary>
    public const string Default = "Roboto";

    private static readonly Dictionary<string, FontFamily> RegisteredTypefaces = new()
    {
        {
            Default,
            new FontFamily(Resources.PackUri("/Assets/Fonts/Roboto Flex/#Roboto Flex"), "./#Roboto Flex")
        }
    };

    internal static void Register(string name, FontFamily typeface)
    {
        if (!RegisteredTypefaces.TryAdd(name, typeface))
        {
            throw new InvalidOperationException($"The typeface '{name}' is already registered.");
        }
    }

    internal static FontFamily GetTypefaceOrDefault(string? name) => string.IsNullOrWhiteSpace(name)
        ? RegisteredTypefaces[Default]
        : RegisteredTypefaces.GetValueOrDefault(name, RegisteredTypefaces[Default]);
}