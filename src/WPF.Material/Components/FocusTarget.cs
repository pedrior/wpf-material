namespace WPF.Material.Components;

/// <summary>
/// Specifies the target element from which the <see cref="KeyboardFocusIndicator"/> should listen for focus events.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public enum FocusTarget
{
    /// <summary>
    /// Indicates that the <see cref="KeyboardFocusIndicator"/> should listen for focus events from the child element.
    /// </summary>
    Child,
    
    /// <summary>
    /// Indicates that the <see cref="KeyboardFocusIndicator"/> should listen for focus events from the parent element.
    /// </summary>
    /// <remarks>
    /// This option tries to listen for focus events from the templated parent first, if available; otherwise, it tries
    /// listens for focus events from the logical parent.
    /// </remarks>
    Parent
}