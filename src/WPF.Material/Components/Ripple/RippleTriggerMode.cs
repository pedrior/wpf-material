namespace WPF.Material.Components;

/// <summary>
/// Specifies the trigger mode of the ripple effect.
/// </summary>
public enum RippleTriggerMode
{
    /// <summary>
    /// Indicates that the ripple effect is triggered manually by calling the <see cref="Ripple.Start"/> method.
    /// </summary>
    Manual,

    /// <summary>
    /// Indicates that the ripple effect is triggered by a mouse press event.
    /// </summary>
    MousePress,

    /// <summary>
    /// Indicates that the ripple effect is triggered by a mouse release event.
    /// </summary>
    MouseRelease
}