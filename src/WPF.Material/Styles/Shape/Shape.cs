namespace WPF.Material.Styles;

/// <summary>
/// Provides attached properties for managing the shape aspects of a component.
/// </summary>
public static class Shape
{
    /// <summary>
    /// Identifies the Family attached property.
    /// </summary>
    public static readonly DependencyProperty FamilyProperty = DependencyProperty.RegisterAttached(
        "Family",
        typeof(ShapeFamily),
        typeof(Shape),
        new FrameworkPropertyMetadata(ShapeFamily.Rounded, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the Style attached property.
    /// </summary>
    public static readonly DependencyProperty StyleProperty = DependencyProperty.RegisterAttached(
        "Style",
        typeof(ShapeStyle),
        typeof(Shape),
        new PropertyMetadata(ShapeStyle.None));

    /// <summary>
    /// Identifies the Corner attached property.
    /// </summary>
    public static readonly DependencyProperty CornerProperty = DependencyProperty.RegisterAttached(
        "Corner",
        typeof(ShapeCorner),
        typeof(Shape),
        new PropertyMetadata(ShapeCorner.All));

    /// <summary>
    /// Identifies the ShapeRadius attached property.
    /// </summary>
    public static readonly DependencyProperty RadiusProperty = DependencyProperty.RegisterAttached(
        "Radius",
        typeof(CornerRadius?),
        typeof(Shape),
        new PropertyMetadata(default(CornerRadius?)));

    /// <summary>
    /// Identifies the UseStyleOnRadiusOverride attached property.
    /// </summary>
    public static readonly DependencyProperty UseStyleOnRadiusOverrideProperty = DependencyProperty.RegisterAttached(
        "UseStyleOnRadiusOverride",
        typeof(bool),
        typeof(Shape),
        new PropertyMetadata(true));

    /// <summary>
    /// Identifies the UseCornersOnRadiusOverride attached property.
    /// </summary>
    public static readonly DependencyProperty UseCornersOnRadiusOverrideProperty = DependencyProperty
        .RegisterAttached(
            "UseCornersOnRadiusOverride",
            typeof(ShapeCorner),
            typeof(Shape),
            new PropertyMetadata(ShapeCorner.All));

    /// <summary>
    /// Sets the value of the <see cref="FamilyProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="FamilyProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetFamily(DependencyObject element, ShapeFamily value) =>
        element.SetValue(FamilyProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="FamilyProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="FamilyProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="FamilyProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static ShapeFamily GetFamily(DependencyObject element) => (ShapeFamily)element.GetValue(FamilyProperty);

    /// <summary>
    /// Sets the value of the <see cref="StyleProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="StyleProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetStyle(DependencyObject element, ShapeStyle value) => element.SetValue(StyleProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="StyleProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="StyleProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="StyleProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static ShapeStyle GetStyle(DependencyObject element) => (ShapeStyle)element.GetValue(StyleProperty);

    /// <summary>
    /// Sets the value of the <see cref="CornerProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="CornerProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetCorner(DependencyObject element, ShapeCorner value) =>
        element.SetValue(CornerProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="CornerProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="CornerProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="CornerProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static ShapeCorner GetCorner(DependencyObject element) => (ShapeCorner)element.GetValue(CornerProperty);

    /// <summary>
    /// Sets the value of the <see cref="RadiusProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="RadiusProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetRadius(DependencyObject element, CornerRadius value) =>
        element.SetValue(RadiusProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="RadiusProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="RadiusProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="RadiusProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static CornerRadius? GetRadius(DependencyObject element) => (CornerRadius?)element.GetValue(RadiusProperty);

    /// <summary>
    /// Sets the value of the <see cref="UseStyleOnRadiusOverrideProperty"/> attached property for a specified
    /// dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="UseStyleOnRadiusOverrideProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetUseStyleOnRadiusOverride(DependencyObject element, bool value) =>
        element.SetValue(UseStyleOnRadiusOverrideProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="UseStyleOnRadiusOverrideProperty"/> attached property for a specified
    /// dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="UseStyleOnRadiusOverrideProperty"/>
    /// property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="UseStyleOnRadiusOverrideProperty"/> attached property on the specified
    /// dependency object.
    /// </returns>
    public static bool GetUseStyleOnRadiusOverride(DependencyObject element) =>
        (bool)element.GetValue(UseStyleOnRadiusOverrideProperty);

    /// <summary>
    /// Sets the value of the <see cref="UseCornersOnRadiusOverrideProperty"/> attached property for a specified
    /// dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="UseCornersOnRadiusOverrideProperty"/>
    /// property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetUseCornersOnRadiusOverride(DependencyObject element, ShapeCorner value) =>
        element.SetValue(UseCornersOnRadiusOverrideProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="UseCornersOnRadiusOverrideProperty"/> attached property for a specified
    /// dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="UseCornersOnRadiusOverrideProperty"/>
    /// property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="UseCornersOnRadiusOverrideProperty"/> attached property on the specified
    /// dependency object.
    /// </returns>
    public static ShapeCorner GetUseCornersOnRadiusOverride(DependencyObject element) =>
        (ShapeCorner)element.GetValue(UseCornersOnRadiusOverrideProperty);
}