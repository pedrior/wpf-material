namespace WPF.Material.Components;

/// <summary>
/// Specifies the release modes of the ripple effect.
/// </summary>
public enum RippleReleaseMode
{
    /// <summary>
    /// Indicates that the ripple effect is released automatically after the trigger event.
    /// </summary>
    Auto,

    /// <summary>
    /// Indicates that the ripple effect is released manually by calling the <see cref="Ripple.Release"/>
    /// method.
    /// </summary>
    Manual
}