namespace WPF.Material.Foundations;

/// <summary>
/// Represents the density levels, which is applied to the control's height. Each level reduces the height of the
/// control by 4px.
/// </summary>
public enum Density
{
    /// <summary>
    /// Indicates the control should have no density applied. 
    /// </summary>
    Level0,
    
    /// <summary>
    /// Indicates the control should have a density of 4px applied.
    /// </summary>
    Level1,
    
    /// <summary>
    /// Indicates the control should have a density of 8px applied.
    /// </summary>
    Level2,
    
    /// <summary>
    /// Indicates the control should have a density of 12px applied.
    /// </summary>
    Level3
}