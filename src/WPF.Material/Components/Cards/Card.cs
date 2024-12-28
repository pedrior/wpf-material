using System.Windows.Input;

namespace WPF.Material.Components;

/// <summary>
/// Represents a container for content and actions related to a single subject.
/// </summary>
[DefaultEvent(nameof(Click))]
public class Card : ContentControl
{
    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type),
        typeof(CardType),
        typeof(Card),
        new PropertyMetadata(CardType.Outlined));

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
    /// Gets or sets the type of the card, which determines its visual appearance. The default value is
    /// <see cref="CardType.Outlined"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public CardType Type
    {
        get => (CardType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the card is clickable. The default value is <see langword="true"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsClickable
    {
        get => (bool)GetValue(IsClickableProperty);
        set => SetValue(IsClickableProperty, value);
    }

    /// <summary>
    /// Gets a value indicating whether the card has been pressed. This is always <see langword="false"/> if the card is
    /// not clickable.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Miscellaneous)]
    public bool IsPressed
    {
        get => (bool)GetValue(IsPressedProperty);
        private set => SetValue(IsPressedPropertyKey, value);
    }

    /// <summary>
    /// Raises the <see cref="Click"/> event.
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