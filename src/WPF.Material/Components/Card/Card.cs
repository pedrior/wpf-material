using System.Windows.Input;

namespace WPF.Material.Components;

/// <summary>
/// Represents a content container for text, media, and actions in the context of a single subject.
/// </summary>
[DefaultEvent(nameof(Click))]
public class Card : ContentControl
{
    /// <summary>
    /// Identifies the <see cref="Appearance"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AppearanceProperty = DependencyProperty.Register(
        nameof(Appearance),
        typeof(CardAppearance),
        typeof(Card),
        new PropertyMetadata(CardAppearance.Outlined));

    /// <summary>
    /// Identifies the <see cref="IsClickable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsClickableProperty = DependencyProperty.Register(
        nameof(IsClickable),
        typeof(bool),
        typeof(Card),
        new PropertyMetadata(true));

    private static readonly DependencyPropertyKey IsPressedPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(IsPressed),
        typeof(bool),
        typeof(Card),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsPressed"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsPressedProperty = IsPressedPropertyKey.DependencyProperty;

    /// <summary>
    /// Identifies the <see cref="Click"/> routed event.
    /// </summary>
    public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
        nameof(Click),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(Card));

    static Card()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
    }

    /// <summary>
    /// Occurs when the card is clicked.
    /// </summary>
    [Category(UICategory.Behavior)]
    public event RoutedEventHandler Click
    {
        add => AddHandler(ClickEvent, value);
        remove => RemoveHandler(ClickEvent, value);
    }

    /// <summary>
    /// Gets or sets the appearance of the card.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public CardAppearance Appearance
    {
        get => (CardAppearance)GetValue(AppearanceProperty);
        set => SetValue(AppearanceProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the card reacts to mouse input. When set to <see langword="false"/>,
    /// the card won't trigger the ripple effect or respond to state changes.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsClickable
    {
        get => (bool)GetValue(IsClickableProperty);
        set => SetValue(IsClickableProperty, value);
    }

    /// <summary>
    /// Gets a value indicating whether the left mouse button is pressed over the card.
    /// </summary>
    /// <remarks>
    /// This property won't change if <see cref="IsClickable"/> is set to <see langword="false"/>.
    /// </remarks>
    [Bindable(true)]
    public bool IsPressed
    {
        get => (bool)GetValue(IsPressedProperty);
        private set => SetValue(IsPressedPropertyKey, value);
    }

    /// <summary>
    /// Invoked when an unhandled <see cref="ClickEvent"/> routed event reaches an element in its route that is derived
    /// from this class. Implement this method to add class handling for this event
    /// </summary>
    protected virtual void OnClick() => RaiseEvent(new RoutedEventArgs(ClickEvent, this));

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        e.Handled = true;

        Focus();

        if (IsClickable && e.ButtonState is MouseButtonState.Pressed)
        {
            CaptureMouse();

            if (IsMouseCaptured)
            {
                IsPressed = true;
            }
        }
        else
        {
            ReleaseMouseCapture();
        }

        base.OnMouseLeftButtonDown(e);
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        e.Handled = true;

        if (IsClickable && IsMouseCaptured)
        {
            ReleaseMouseCapture();
            OnClick();
        }

        base.OnMouseLeftButtonUp(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (IsClickable && IsMouseCaptured && Mouse.LeftButton is MouseButtonState.Pressed)
        {
            UpdateIsPressed();

            e.Handled = true;
        }
    }

    protected override void OnLostMouseCapture(MouseEventArgs e)
    {
        base.OnLostMouseCapture(e);

        if (Equals(e.OriginalSource, this))
        {
            IsPressed = false;
        }
    }
    
    private void UpdateIsPressed()
    {
        // Check if the mouse is within the bounds of the card.
        var position = Mouse.GetPosition(this);
        var isWithin = position.X >= 0 && position.X <= ActualWidth &&
                       position.Y >= 0 && position.Y <= ActualHeight;

        IsPressed = isWithin;
    }
}