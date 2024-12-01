namespace WPF.Material.Assists;

/// <summary>
/// Provides attached properties for layout management.
/// </summary>
public static class LayoutAssist
{
    /// <summary>
    /// Identifies the IsDense attached property.
    /// </summary>
    public static readonly DependencyProperty IsDenseProperty = DependencyProperty.RegisterAttached(
        "IsDense",
        typeof(bool),
        typeof(LayoutAssist),
        new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the Density attached property.
    /// </summary>
    public static readonly DependencyProperty DensityProperty = DependencyProperty.RegisterAttached(
        "Density",
        typeof(Density),
        typeof(LayoutAssist),
        new FrameworkPropertyMetadata(Density.Level0, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the ContainerWidth attached property.
    /// </summary>
    public static readonly DependencyProperty ContainerWidthProperty = DependencyProperty.RegisterAttached(
        "ContainerWidth",
        typeof(double),
        typeof(LayoutAssist),
        new PropertyMetadata(default(double)));

    /// <summary>
    /// Identifies the ContainerHeight attached property.
    /// </summary>
    public static readonly DependencyProperty ContainerHeightProperty = DependencyProperty.RegisterAttached(
        "ContainerHeight",
        typeof(double),
        typeof(LayoutAssist),
        new PropertyMetadata(default(double)));

    /// <summary>
    /// Sets the value of the <see cref="IsDenseProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsDenseProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsDense(DependencyObject element, bool value) =>
        element.SetValue(IsDenseProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsDenseProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsDenseProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsDenseProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetIsDense(DependencyObject element) =>
        (bool)element.GetValue(IsDenseProperty);

    /// <summary>
    /// Sets the value of the <see cref="DensityProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="DensityProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetDensity(DependencyObject element, Density value) => element.SetValue(DensityProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="DensityProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="DensityProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="DensityProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static Density GetDensity(DependencyObject element) => (Density)element.GetValue(DensityProperty);
    
    /// <summary>
    /// Sets the value of the <see cref="ContainerWidthProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="ContainerWidthProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetContainerWidth(DependencyObject element, double value) =>
        element.SetValue(ContainerWidthProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="ContainerWidthProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="ContainerWidthProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="ContainerWidthProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static double GetContainerWidth(DependencyObject element) =>
        (double)element.GetValue(ContainerWidthProperty);

    /// <summary>
    /// Sets the value of the <see cref="ContainerHeightProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="ContainerHeightProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetContainerHeight(DependencyObject element, double value) =>
        element.SetValue(ContainerHeightProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="ContainerHeightProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="ContainerHeightProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="ContainerHeightProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static double GetContainerHeight(DependencyObject element) =>
        (double)element.GetValue(ContainerHeightProperty);
}