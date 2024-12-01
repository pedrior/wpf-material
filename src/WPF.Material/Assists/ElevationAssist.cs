namespace WPF.Material.Assists;

/// <summary>
/// Provides attached properties for elevation management.
/// </summary>
public static class ElevationAssist
{
    /// <summary>
    /// Identifies the Level attached property.
    /// </summary>
    public static readonly DependencyProperty LevelProperty = DependencyProperty.RegisterAttached(
        "Level",
        typeof(ElevationLevel),
        typeof(ElevationAssist),
        new PropertyMetadata(ElevationLevel.None));

    /// <summary>
    /// Sets the value of the <see cref="LevelProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="LevelProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetLevel(DependencyObject element, ElevationLevel value) =>
        element.SetValue(LevelProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="LevelProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="LevelProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="LevelProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static ElevationLevel GetLevel(DependencyObject element) =>
        (ElevationLevel)element.GetValue(LevelProperty);
}