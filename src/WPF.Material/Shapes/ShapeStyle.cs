namespace WPF.Material.Shapes;

/// <summary>
/// Identifies the styles that can be applied to a shape.
/// </summary>
public enum ShapeStyle
{
    /// <summary>
    /// Indicates that no style is applied and the shape will have sharp corners.
    /// </summary>
    None,
    
    /// <summary>
    /// Indicates that the shape will have corners with a radius of 4.
    /// </summary>
    ExtraSmall,
    
    /// <summary>
    /// Indicates that the shape will have corners with a radius of 8.
    /// </summary>
    Small,

    /// <summary>
    /// Indicates that the shape will have corners with a radius of 12.
    /// </summary>
    Medium,
    
    /// <summary>
    /// Indicates that the shape will have corners with a radius of 16.
    /// </summary>
    Large,

    /// <summary>
    /// Indicates that the shape will have corners with a radius of 28.
    /// </summary>
    ExtraLarge,

    /// <summary>
    /// Indicates that the shape will have corners with a radius equal to the smallest dimension of the shape.
    /// </summary>
    Full
}