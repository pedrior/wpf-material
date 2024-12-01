namespace WPF.Material.Shapes;

/// <summary>
/// Identifies the corners of a shape. A shape will always have corners, this enum indicates which corners are part of
/// the shape's style.
/// </summary>
[Flags]
public enum ShapeCorner
{
    /// <summary>
    /// Indicates that no corners are part of the shape's style.
    /// </summary>
    None = 0x0,
    
    /// <summary>
    /// Indicates that the top-left corner is part of the shape's style.
    /// </summary>
    TopLeft = 0x1,
    
    /// <summary>
    /// Indicates that the top-right corner is part of the shape's style.
    /// </summary>
    TopRight = 0x2,
    
    /// <summary>
    /// Indicates that the bottom-left corner is part of the shape's style.
    /// </summary>
    BottomLeft = 0x4,
    
    /// <summary>
    /// Indicates that the bottom-right corner is part of the shape's style.
    /// </summary>
    BottomRight = 0x8,

    /// <summary>
    /// Indicates that all corners are part of the shape's style.
    /// </summary>
    All = TopLeft | TopRight | BottomLeft | BottomRight
}