using System.Windows.Input;

namespace WPF.Material.Components;

/// <summary>
/// Represents a control that draws an indicator around a focused element when it has keyboard focus.
/// </summary>
/// <remarks>
/// A UI element can receive keyboard focus by being navigated with a key or by being clicked with the mouse. That
/// said, this focus indicator is restricted to the keyboard device, i.e. no indicator is drawn when the device that
/// caused the element to be focused on is not the keyboard.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public class KeyboardFocusIndicator : FocusIndicator
{
    static KeyboardFocusIndicator()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(KeyboardFocusIndicator),
            new FrameworkPropertyMetadata(typeof(KeyboardFocusIndicator)));
    }

    protected override void SubscribeToTargetElementEvents(FrameworkElement? element)
    {
        if (element is not null)
        {
            element.GotFocus += OnTargetGotFocus;
            element.LostFocus += OnTargetLostFocus;
        }

        base.SubscribeToTargetElementEvents(element);
    }

    protected override void UnsubscribeFromTargetElementEvents(FrameworkElement? element)
    {
        if (element is not null)
        {
            element.GotFocus -= OnTargetGotFocus;
            element.LostFocus -= OnTargetLostFocus;
        }

        base.UnsubscribeFromTargetElementEvents(element);
    }

    private static bool IsKeyboardMostRecentInputDevice()
    {
        // Checking for the most recent input device replicates the behavior of the default FocusVisualStyle handler.
        // This is necessary because the IsKeyboardFocused property is not restricted to the keyboard device, i.e. it
        // returns true even if the element was focused with the mouse.
        return InputManager.Current.MostRecentInputDevice is KeyboardDevice;
    }

    private void OnTargetGotFocus(object sender, RoutedEventArgs e) => NotifyFocus(IsKeyboardMostRecentInputDevice());

    private void OnTargetLostFocus(object sender, RoutedEventArgs e) => NotifyFocus(false);
}