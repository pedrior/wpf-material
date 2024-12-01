namespace WPF.Material.Controls;

/// <summary>
/// Represents the base class for toggle buttons, which are controls that can switch between two states.
/// </summary>
/// <remarks>
/// Setting the <see cref="System.Windows.Controls.Primitives.ToggleButton.IsThreeState"/> property to
/// <see langword="true"/> will have no effect on this control.
/// </remarks>
public abstract class ToggleButtonBase : System.Windows.Controls.Primitives.ToggleButton
{
    static ToggleButtonBase()
    {
        // We don't support the three state behavior
        IsThreeStateProperty.OverrideMetadata(
            typeof(ToggleIconButton),
            new FrameworkPropertyMetadata(false, null, CoerceIsThreeState));
    }
    
    private static object CoerceIsThreeState(DependencyObject element, object value) => false;
}