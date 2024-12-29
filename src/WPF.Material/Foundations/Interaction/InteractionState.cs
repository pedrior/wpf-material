namespace WPF.Material.Foundations;

/// <summary>
/// Specifies the interaction state of a UI element.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public enum InteractionState
{
    /// <summary>
    /// Indicates that the user is not interacting with the element.
    /// </summary>
    None,
    
    /// <summary>
    /// Indicates that the user is hovering over the element.
    /// </summary>
    Hovered,
    
    /// <summary>
    /// Indicates that the user has pressed the element using the mouse or keyboard.
    /// </summary>
    Pressed,
    
    /// <summary>
    /// Indicates that the user is dragging the element with the mouse.
    /// </summary>
    Dragged
}