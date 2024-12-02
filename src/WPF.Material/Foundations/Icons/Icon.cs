using WPF.Material.Components;
using WPF.Material.Controls;

namespace WPF.Material.Foundations;

/// <summary>
/// Provides attached properties for managing the iconography aspects of a component.
/// </summary>
public static class Icon
{
    /// <summary>
    /// Identifies the Icon attached property.
    /// </summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
        "Icon",
        typeof(Symbol?),
        typeof(Icon),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the Size attached property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty = DependencyProperty.RegisterAttached(
        "Size",
        typeof(double),
        typeof(Icon),
        new PropertyMetadata(18.0));

    /// <summary>
    /// Identifies the Fill attached property.
    /// </summary>
    public static readonly DependencyProperty FillProperty = DependencyProperty.RegisterAttached(
        "Fill",
        typeof(bool),
        typeof(Icon),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the Weight attached property.
    /// </summary>
    public static readonly DependencyProperty WeightProperty = DependencyProperty.RegisterAttached(
        "Weight",
        typeof(FontWeight),
        typeof(Icon),
        new PropertyMetadata(FontWeights.Normal));

    /// <summary>
    /// Identifies the Style attached property.
    /// </summary>
    public static readonly DependencyProperty StyleProperty = DependencyProperty.RegisterAttached(
        "Style",
        typeof(SymbolStyle),
        typeof(Icon),
        new FrameworkPropertyMetadata(SymbolStyle.Rounded, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the IconOnHovering attached property.
    /// </summary>
    public static readonly DependencyProperty IconOnHoveringProperty = DependencyProperty.RegisterAttached(
        "IconOnHovering",
        typeof(Symbol?),
        typeof(Icon),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the IconOnPressing attached property.
    /// </summary>
    public static readonly DependencyProperty IconOnPressingProperty = DependencyProperty.RegisterAttached(
        "IconOnPressing",
        typeof(Symbol?),
        typeof(Icon),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the IconOnSelecting attached property.
    /// </summary>
    public static readonly DependencyProperty IconOnSelectingProperty = DependencyProperty.RegisterAttached(
        "IconOnSelecting",
        typeof(Symbol?),
        typeof(Icon),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the FillOnHovering attached property.
    /// </summary>
    public static readonly DependencyProperty FillOnHoveringProperty = DependencyProperty.RegisterAttached(
        "FillOnHovering",
        typeof(bool),
        typeof(Icon),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the FillOnPressing attached property.
    /// </summary>
    public static readonly DependencyProperty FillOnPressingProperty = DependencyProperty.RegisterAttached(
        "FillOnPressing",
        typeof(bool),
        typeof(Icon),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the FillOnSelecting attached property.
    /// </summary>
    public static readonly DependencyProperty FillOnSelectingProperty = DependencyProperty.RegisterAttached(
        "FillOnSelecting",
        typeof(bool),
        typeof(Icon),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the WeightOnHovering attached property.
    /// </summary>
    public static readonly DependencyProperty WeightOnHoveringProperty = DependencyProperty.RegisterAttached(
        "WeightOnHovering",
        typeof(FontWeight),
        typeof(Icon),
        new PropertyMetadata(FontWeights.Normal));

    /// <summary>
    /// Identifies the WeightOnPressing attached property.
    /// </summary>
    public static readonly DependencyProperty WeightOnPressingProperty = DependencyProperty.RegisterAttached(
        "WeightOnPressing",
        typeof(FontWeight),
        typeof(Icon),
        new PropertyMetadata(FontWeights.Normal));

    /// <summary>
    /// Identifies the WeightOnSelecting attached property.
    /// </summary>
    public static readonly DependencyProperty WeightOnSelectingProperty = DependencyProperty.RegisterAttached(
        "WeightOnSelecting",
        typeof(FontWeight),
        typeof(Icon),
        new PropertyMetadata(FontWeights.Normal));

    public static Symbol? GetIcon(DependencyObject element) => (Symbol?)element.GetValue(IconProperty);

    /// <summary>
    /// Sets the value of the <see cref="IconProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IconProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIcon(DependencyObject element, Symbol? value) => element.SetValue(IconProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="SizeProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="SizeProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="SizeProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static double GetSize(DependencyObject element) => (double)element.GetValue(SizeProperty);

    /// <summary>
    /// Sets the value of the <see cref="SizeProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="SizeProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetSize(DependencyObject element, double value) => element.SetValue(SizeProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="FillProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="FillProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="FillProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetFill(DependencyObject element) => (bool)element.GetValue(FillProperty);

    /// <summary>
    /// Sets the value of the <see cref="FillProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="FillProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetFill(DependencyObject element, bool value) => element.SetValue(FillProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="WeightProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="WeightProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="WeightProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static FontWeight GetWeight(DependencyObject element) => (FontWeight)element.GetValue(WeightProperty);

    /// <summary>
    /// Sets the value of the <see cref="WeightProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="WeightProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetWeight(DependencyObject element, FontWeight value) => element.SetValue(WeightProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="StyleProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="StyleProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="StyleProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static SymbolStyle GetStyle(DependencyObject element) => (SymbolStyle)element.GetValue(StyleProperty);

    /// <summary>
    /// Sets the value of the <see cref="StyleProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="StyleProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetStyle(DependencyObject element, SymbolStyle value) => element.SetValue(StyleProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IconOnHoveringProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IconOnHoveringProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IconOnHoveringProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static Symbol? GetIconOnHovering(DependencyObject element) =>
        (Symbol?)element.GetValue(IconOnHoveringProperty);

    /// <summary>
    /// Sets the value of the <see cref="IconOnHoveringProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IconOnHoveringProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIconOnHovering(DependencyObject element, Symbol? value) =>
        element.SetValue(IconOnHoveringProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IconOnPressingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IconOnPressingProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IconOnPressingProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static Symbol? GetIconOnPressing(DependencyObject element) =>
        (Symbol?)element.GetValue(IconOnPressingProperty);

    /// <summary>
    /// Sets the value of the <see cref="IconOnPressingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IconOnPressingProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIconOnPressing(DependencyObject element, Symbol? value) =>
        element.SetValue(IconOnPressingProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IconOnSelectingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IconOnSelectingProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IconOnSelectingProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static Symbol? GetIconOnSelecting(DependencyObject element) =>
        (Symbol?)element.GetValue(IconOnSelectingProperty);

    /// <summary>
    /// Sets the value of the <see cref="IconOnSelectingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IconOnSelectingProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIconOnSelecting(DependencyObject element, Symbol? value) =>
        element.SetValue(IconOnSelectingProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="FillOnHoveringProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="FillOnHoveringProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="FillOnHoveringProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetFillOnHovering(DependencyObject element) => (bool)element.GetValue(FillOnHoveringProperty);

    /// <summary>
    /// Sets the value of the <see cref="FillOnHoveringProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="FillOnHoveringProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetFillOnHovering(DependencyObject element, bool value) =>
        element.SetValue(FillOnHoveringProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="FillOnPressingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="FillOnPressingProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="FillOnPressingProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetFillOnPressing(DependencyObject element) => (bool)element.GetValue(FillOnPressingProperty);

    /// <summary>
    /// Sets the value of the <see cref="FillOnPressingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="FillOnPressingProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetFillOnPressing(DependencyObject element, bool value) =>
        element.SetValue(FillOnPressingProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="FillOnSelectingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="FillOnSelectingProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="FillOnSelectingProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetFillOnSelecting(DependencyObject element) => (bool)element.GetValue(FillOnSelectingProperty);

    /// <summary>
    /// Sets the value of the <see cref="FillOnSelectingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="FillOnSelectingProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetFillOnSelecting(DependencyObject element, bool value) =>
        element.SetValue(FillOnSelectingProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="WeightOnHoveringProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="WeightOnHoveringProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="WeightOnHoveringProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static FontWeight GetWeightOnHovering(DependencyObject element) =>
        (FontWeight)element.GetValue(WeightOnHoveringProperty);

    /// <summary>
    /// Sets the value of the <see cref="WeightOnHoveringProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="WeightOnHoveringProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetWeightOnHovering(DependencyObject element, FontWeight value) =>
        element.SetValue(WeightOnHoveringProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="WeightOnPressingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="WeightOnPressingProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="WeightOnPressingProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static FontWeight GetWeightOnPressing(DependencyObject element) =>
        (FontWeight)element.GetValue(WeightOnPressingProperty);

    /// <summary>
    /// Sets the value of the <see cref="WeightOnPressingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="WeightOnPressingProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetWeightOnPressing(DependencyObject element, FontWeight value) =>
        element.SetValue(WeightOnPressingProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="WeightOnSelectingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="WeightOnSelectingProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="WeightOnSelectingProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static FontWeight GetWeightOnSelecting(DependencyObject element) =>
        (FontWeight)element.GetValue(WeightOnSelectingProperty);

    /// <summary>
    /// Sets the value of the <see cref="WeightOnSelectingProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="WeightOnSelectingProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetWeightOnSelecting(DependencyObject element, FontWeight value) =>
        element.SetValue(WeightOnSelectingProperty, value);
}