﻿namespace WPF.Material.Foundations;

/// <summary>
/// Provides attached properties for managing the interaction aspects of a component.
/// </summary>
public static class Interaction
{
    /// <summary>
    /// Identifies the IsHovered attached property.
    /// </summary>
    public static readonly DependencyProperty IsHoveredProperty = DependencyProperty.RegisterAttached(
        "IsHovered",
        typeof(bool),
        typeof(Interaction),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the IsPressed attached property.
    /// </summary>
    public static readonly DependencyProperty IsPressedProperty = DependencyProperty.RegisterAttached(
        "IsPressed",
        typeof(bool),
        typeof(Interaction),
        new PropertyMetadata(false));
    
    /// <summary>
    /// Identifies the IsFocused attached property.
    /// </summary>
    public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached(
        "IsFocused",
        typeof(bool),
        typeof(Interaction),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the IsDragged attached property.
    /// </summary>
    public static readonly DependencyProperty IsDraggedProperty = DependencyProperty.RegisterAttached(
        "IsDragged",
        typeof(bool),
        typeof(Interaction),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the IsEnabled attached property.
    /// </summary>
    public static readonly DependencyProperty IsInteractiveProperty = DependencyProperty.RegisterAttached(
        "IsInteractive",
        typeof(bool),
        typeof(Interaction),
        new PropertyMetadata(true));

    /// <summary>
    /// Identifies the IsRippleAnimated attached property.
    /// </summary>
    public static readonly DependencyProperty IsRippleAnimatedProperty = DependencyProperty.RegisterAttached(
        "IsRippleAnimated",
        typeof(bool),
        typeof(Interaction),
        new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the IsRippleCentered attached property.
    /// </summary>
    public static readonly DependencyProperty IsRippleCenteredProperty = DependencyProperty.RegisterAttached(
        "IsRippleCentered",
        typeof(bool),
        typeof(Interaction),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the IsRippleUnbounded attached property.
    /// </summary>
    public static readonly DependencyProperty IsRippleUnboundedProperty = DependencyProperty.RegisterAttached(
        "IsRippleUnbounded",
        typeof(bool),
        typeof(Interaction),
        new PropertyMetadata(false));

    /// <summary>
    /// Sets the value of the <see cref="IsHoveredProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsHoveredProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsHovered(DependencyObject element, bool value) => element.SetValue(IsHoveredProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsHoveredProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsHoveredProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsHoveredProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetIsHovered(DependencyObject element) => (bool)element.GetValue(IsHoveredProperty);

    /// <summary>
    /// Sets the value of the <see cref="IsPressedProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsPressedProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsPressed(DependencyObject element, bool value) => element.SetValue(IsPressedProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsPressedProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsPressedProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsPressedProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetIsPressed(DependencyObject element) => (bool)element.GetValue(IsPressedProperty);
    
    /// <summary>
    /// Sets the value of the <see cref="IsFocusedProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsFocusedProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsFocused(DependencyObject element, bool value) => element.SetValue(IsFocusedProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsFocusedProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsFocusedProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsFocusedProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetIsFocused(DependencyObject element) => (bool)element.GetValue(IsFocusedProperty);

    /// <summary>
    /// Sets the value of the <see cref="IsDraggedProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsDraggedProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsDragged(DependencyObject element, bool value) => element.SetValue(IsDraggedProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsDraggedProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsDraggedProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsDraggedProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetIsDragged(DependencyObject element) => (bool)element.GetValue(IsDraggedProperty);
    
    /// <summary>
    /// Sets the value of the <see cref="IsInteractiveProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsInteractiveProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsInteractive(DependencyObject element, bool value) => 
        element.SetValue(IsInteractiveProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsInteractiveProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsInteractiveProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsInteractiveProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetIsInteractive(DependencyObject element) => (bool)element.GetValue(IsInteractiveProperty);
    
    /// <summary>
    /// Sets the value of the <see cref="IsRippleAnimatedProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsRippleAnimatedProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsRippleAnimated(DependencyObject element, bool value) => 
        element.SetValue(IsRippleAnimatedProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsRippleAnimatedProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsRippleAnimatedProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsRippleAnimatedProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetIsRippleAnimated(DependencyObject element) =>
        (bool)element.GetValue(IsRippleAnimatedProperty);

    /// <summary>
    /// Sets the value of the <see cref="IsRippleCenteredProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsRippleCenteredProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsRippleCentered(DependencyObject element, bool value) => 
        element.SetValue(IsRippleCenteredProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsRippleCenteredProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsRippleCenteredProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsRippleCenteredProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetIsRippleCentered(DependencyObject element) => 
        (bool)element.GetValue(IsRippleCenteredProperty);

    /// <summary>
    /// Sets the value of the <see cref="IsRippleUnboundedProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IsRippleUnboundedProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIsRippleUnbounded(DependencyObject element, bool value) => 
        element.SetValue(IsRippleUnboundedProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsRippleUnboundedProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IsRippleUnboundedProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IsRippleUnboundedProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static bool GetIsRippleUnbounded(DependencyObject element) => 
        (bool)element.GetValue(IsRippleUnboundedProperty);
}