namespace WPF.Material.Controls;

/// <summary>
/// Represents the types of a button.
/// </summary>
public enum ButtonType
{
    /// <summary>
    /// Identifies the filled button, which contrasts with the surface color.
    /// </summary>
    Filled,

    /// <summary>
    /// Identifies the tonal button, which has a lighter background color and darker label color.
    /// </summary>
    Tonal,

    /// <summary>
    /// Identifies the elevated button, which has a lighter background color and shadow.
    /// </summary>
    Elevated,

    /// <summary>
    /// Identifies the outlined button, which has a transparent background and a border.
    /// </summary>
    Outlined,

    /// <summary>
    /// Identifies the text button, which has a transparent background and no border.
    /// </summary>
    Text
}