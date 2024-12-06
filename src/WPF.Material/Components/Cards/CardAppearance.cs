namespace WPF.Material.Components;

/// <summary>
/// Represents the appearance of a <see cref="Card"/> component.
/// </summary>
public enum CardAppearance
{
    /// <summary>
    /// Indicates that the card has a visual boundary around its container.
    /// </summary>
    Outlined,
    
    /// <summary>
    /// Indicates that the card has a subtle separation from the background.
    /// </summary>
    Filled,
    
    /// <summary>
    /// Indicates that the card has a drop shadow, providing more separation from the background than filled cards,
    /// but less than outlined cards.
    /// </summary>
    Elevated
}