namespace WPF.Material.Components;

/// <summary>
/// Specifies the types of icon buttons, which determine their appearance. All icon buttons are considered low-emphasis.
/// </summary>
public enum IconButtonType
{
    /// <summary>
    /// Identifies an icon button with a transparent background and no border.
    /// </summary>
    Standard,

    /// <summary>
    /// Identifies an icon button that contrasts with the surface color.
    /// </summary>
    Filled,

    /// <summary>
    /// Identifies an icon button featuring a lighter background and darker label color.
    /// </summary>
    Tonal,

    /// <summary>
    /// Identifies an icon button with a transparent background and border.
    /// </summary>
    Outlined
}