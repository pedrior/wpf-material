namespace WPF.Material.Components;

/// <summary>
/// Specifies the types of cards, which determine their appearance.
/// </summary>
public enum CardType
{
    /// <summary>
    /// Indicates a card with a border around the content.
    /// </summary>
    Outlined,

    /// <summary>
    /// Indicates card with a subtle separation from the background.
    /// </summary>
    Filled,

    /// <summary>
    /// Indicates card with a shadow, providing more separation from the background than filled cards,
    /// but less than outlined cards.
    /// </summary>
    Elevated
}