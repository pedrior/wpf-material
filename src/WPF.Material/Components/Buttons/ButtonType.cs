namespace WPF.Material.Components;

/// <summary>
/// <para>
/// Specifies the types of buttons, which determine their appearance and emphasis.
/// </para>
/// 
/// <para>
/// • High-emphasis buttons (<see cref="ButtonType.Filled"/>) are typically used for the primary, most important, or
/// most common actions on a screen. Examples include "Save", "Confirm", or "Done"
/// </para>
/// 
/// <para>
/// • Medium-emphasis buttons (<see cref="ButtonType.Tonal"/>, <see cref="ButtonType.Elevated"/> and
/// <see cref="ButtonType.Outlined"/>) are typically used for important actions that don't need to be quite as
/// prominent as high-emphasis buttons. Examples include "Cancel", "View all", or "Reply".
/// </para>
/// 
/// <para>
/// • Low-emphasis buttons (<see cref="ButtonType.Text"/>) are typically used for optimal or supplementary actions that
/// are less prominent than medium-emphasis buttons. Examples include "Learn more", "Change account", or "Yes/No/Maybe".
/// </para>
/// </summary>
public enum ButtonType
{
    /// <summary>
    /// Identifies a high-emphasis button that contrasts with the surface color.
    /// </summary>
    Filled,

    /// <summary>
    /// Identifies a medium-emphasis button with a light background and dark label.
    /// </summary>
    Tonal,

    /// <summary>
    /// Identifies a medium-emphasis button with a lighter background color and shadow.
    /// </summary>
    Elevated,

    /// <summary>
    /// Identifies a medium-emphasis button with a transparent background and border.
    /// </summary>
    Outlined,

    /// <summary>
    /// Identifies a low-emphasis button with a transparent background and no border.
    /// </summary>
    Text
}