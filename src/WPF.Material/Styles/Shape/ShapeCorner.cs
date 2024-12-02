namespace WPF.Material.Shapes;

/// <summary>
/// Represents the corners of a shape.
/// </summary>
[Flags]
public enum ShapeCorner
{
    /// <summary>
    /// Indicates no corners.
    /// </summary>
    None = 0x0,
    
    /// <summary>
    /// Indicates the top-left corner of a shape.
    /// </summary>
    TopLeft = 0x1,
    
    /// <summary>
    /// Indicates the top-right corner of a shape.
    /// </summary>
    TopRight = 0x2,
    
    /// <summary>
    /// Indicates the bottom-left corner of a shape.
    /// </summary>
    BottomLeft = 0x4,
    
    /// <summary>
    /// Indicates the bottom-right corner of a shape.
    /// </summary>
    BottomRight = 0x8,

    /// <summary>
    /// Indicates all corners of a shape.
    /// </summary>
    All = TopLeft | TopRight | BottomLeft | BottomRight
}