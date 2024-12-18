namespace WPF.Material.Foundations;

/// <summary>
/// Provides attached properties for managing the layout aspects of a component.
/// </summary>
public static class Layout
{
    /// <summary>
    /// Identifies the Density attached property.
    /// </summary>
    public static readonly DependencyProperty DensityProperty = DependencyProperty.RegisterAttached(
        "Density",
        typeof(Density),
        typeof(Layout),
        new FrameworkPropertyMetadata(Density.Level0, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the InnerWidth attached property.
    /// </summary>
    public static readonly DependencyProperty InnerWidthProperty = DependencyProperty.RegisterAttached(
        "InnerWidth",
        typeof(double),
        typeof(Layout),
        new PropertyMetadata(default(double)));

    /// <summary>
    /// Identifies the InnerHeight attached property.
    /// </summary>
    public static readonly DependencyProperty InnerHeightProperty = DependencyProperty.RegisterAttached(
        "InnerHeight",
        typeof(double),
        typeof(Layout),
        new PropertyMetadata(default(double)));

    /// <summary>
    /// Identifies the Spacing attached property.
    /// </summary>
    public static readonly DependencyProperty SpacingProperty = DependencyProperty.RegisterAttached(
        "Spacing",
        typeof(double),
        typeof(Layout),
        new PropertyMetadata(0.0));

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
    /// Sets the value of the <see cref="InnerWidthProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="InnerWidthProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetInnerWidth(DependencyObject element, double value) =>
        element.SetValue(InnerWidthProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="InnerWidthProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="InnerWidthProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="InnerWidthProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static double GetInnerWidth(DependencyObject element) =>
        (double)element.GetValue(InnerWidthProperty);

    /// <summary>
    /// Sets the value of the <see cref="InnerHeightProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="InnerHeightProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetInnerHeight(DependencyObject element, double value) =>
        element.SetValue(InnerHeightProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="InnerHeightProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="InnerHeightProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="InnerHeightProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static double GetInnerHeight(DependencyObject element) =>
        (double)element.GetValue(InnerHeightProperty);
    
    /// <summary>
    /// Sets the value of the <see cref="SpacingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="SpacingProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetSpacing(DependencyObject element, double value) => element.SetValue(SpacingProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="SpacingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="SpacingProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="SpacingProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static double GetSpacing(DependencyObject element) => (double)element.GetValue(SpacingProperty);
}