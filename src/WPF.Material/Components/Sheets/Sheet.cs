using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WPF.Material.Components;

/// <summary>
/// Provides a base class for sheet components. A <see cref="Sheet"/> provides a container for content that slides in
/// and out of the screen.
/// </summary>
public abstract class Sheet : ContentControl
{
    /// <summary>
    /// Identifies the <see cref="IsOpen"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
        nameof(IsOpen),
        typeof(bool),
        typeof(Sheet),
        new PropertyMetadata(false, OnIsOpenChanged));

    /// <summary>
    /// Identifies the <see cref="IsModal"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsModalProperty = DependencyProperty.Register(
        nameof(IsModal),
        typeof(bool),
        typeof(Sheet),
        new PropertyMetadata(false, OnIsModalChanged));

    /// <summary>
    /// Identifies the <see cref="IsDocked"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsDockedProperty = DependencyProperty.Register(
        nameof(IsDocked),
        typeof(bool),
        typeof(Sheet),
        new PropertyMetadata(true, OnIsDockedChanged));

    /// <summary>
    /// Identifies the <see cref="UndockedMargin"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UndockedMarginProperty = DependencyProperty.Register(
        nameof(UndockedMargin),
        typeof(Thickness),
        typeof(Sheet),
        new PropertyMetadata(new Thickness(16.0)));

    /// <summary>
    /// Identifies the <see cref="IsOpenChanged"/> routed event.
    /// </summary>
    public static readonly RoutedEvent IsOpenChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(IsOpenChanged),
        RoutingStrategy.Direct,
        typeof(RoutedPropertyChangedEventHandler<bool>),
        typeof(Sheet));

    /// <summary>
    /// Identifies the <see cref="IsModalChanged"/> routed event.
    /// </summary>
    public static readonly RoutedEvent IsModalChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(IsModalChanged),
        RoutingStrategy.Direct,
        typeof(RoutedPropertyChangedEventHandler<bool>),
        typeof(Sheet));

    /// <summary>
    /// Identifies the <see cref="IsDockedChanged"/> routed event.
    /// </summary>
    public static readonly RoutedEvent IsDockedChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(IsDockedChanged),
        RoutingStrategy.Direct,
        typeof(RoutedPropertyChangedEventHandler<bool>),
        typeof(Sheet));

    /// <summary>
    /// Identifies the <see cref="Opening"/> routed event.
    /// </summary>
    public static readonly RoutedEvent OpeningEvent = EventManager.RegisterRoutedEvent(
        nameof(Opening),
        RoutingStrategy.Direct,
        typeof(RoutedEventHandler),
        typeof(Sheet));

    /// <summary>
    /// Identifies the <see cref="Closing"/> routed event.
    /// </summary>
    public static readonly RoutedEvent ClosingEvent = EventManager.RegisterRoutedEvent(
        nameof(Closing),
        RoutingStrategy.Direct,
        typeof(RoutedEventHandler),
        typeof(Sheet));

    /// <summary>
    /// Identifies the <see cref="Opened"/> routed event.
    /// </summary>
    public static readonly RoutedEvent OpenedEvent = EventManager.RegisterRoutedEvent(
        nameof(Opened),
        RoutingStrategy.Direct,
        typeof(RoutedEventHandler),
        typeof(Sheet));

    /// <summary>
    /// Identifies the <see cref="Closed"/> routed event.
    /// </summary>
    public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
        nameof(Closed),
        RoutingStrategy.Direct,
        typeof(RoutedEventHandler),
        typeof(Sheet));

    private const double ScrimOverlayOpacity = 0.4;

    private static readonly AnimationTimeline enterScrimAnimation = CreateBaseAnimation(ScrimOverlayOpacity);
    private static readonly AnimationTimeline exitScrimAnimation = CreateBaseAnimation(0.0);

    private bool isOpen;
    private bool isModal;
    private bool isDocked = true;

    /// <summary>
    /// Initializes a new instance of <see cref="Sheet"/>.
    /// </summary>
    protected Sheet()
    {
        Loaded += OnLoaded;
    }
    
    /// <summary>
    /// Occurs when the <see cref="IsOpen"/> property changes.
    /// </summary>
    public event RoutedPropertyChangedEventHandler<bool> IsOpenChanged
    {
        add => AddHandler(IsOpenChangedEvent, value);
        remove => RemoveHandler(IsOpenChangedEvent, value);
    }

    /// <summary>
    /// Occurs when the <see cref="IsModal"/> property changes.
    /// </summary>
    public event RoutedPropertyChangedEventHandler<bool> IsModalChanged
    {
        add => AddHandler(IsModalChangedEvent, value);
        remove => RemoveHandler(IsModalChangedEvent, value);
    }

    /// <summary>
    /// Occurs when the <see cref="IsDocked"/> property changes.
    /// </summary>
    public event RoutedPropertyChangedEventHandler<bool> IsDockedChanged
    {
        add => AddHandler(IsDockedChangedEvent, value);
        remove => RemoveHandler(IsDockedChangedEvent, value);
    }

    /// <summary>
    /// Occurs when the <see cref="Sheet"/> is opening.
    /// </summary>
    public event RoutedEventHandler Opening
    {
        add => AddHandler(OpeningEvent, value);
        remove => RemoveHandler(OpeningEvent, value);
    }

    /// <summary>
    /// Occurs when the <see cref="Sheet"/> is closing.
    /// </summary>
    public event RoutedEventHandler Closing
    {
        add => AddHandler(ClosingEvent, value);
        remove => RemoveHandler(ClosingEvent, value);
    }

    /// <summary>
    /// Occurs when the <see cref="Sheet"/> is opened.
    /// </summary>
    public event RoutedEventHandler Opened
    {
        add => AddHandler(OpenedEvent, value);
        remove => RemoveHandler(OpenedEvent, value);
    }

    /// <summary>
    /// Occurs when the <see cref="Sheet"/> is closed.
    /// </summary>
    public event RoutedEventHandler Closed
    {
        add => AddHandler(ClosedEvent, value);
        remove => RemoveHandler(ClosedEvent, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="Sheet"/> is open. The default value is
    /// <see langword="false"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="Sheet"/> should be presented in modal view. The default
    /// value is <see langword="false"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public bool IsModal
    {
        get => (bool)GetValue(IsModalProperty);
        set => SetValue(IsModalProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="Sheet"/> is docked to its parent. When undocked, the
    /// margin defined by the <see cref="UndockedMargin"/> property is applied. The default value is
    /// <see langword="true"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public bool IsDocked
    {
        get => (bool)GetValue(IsDockedProperty);
        set => SetValue(IsDockedProperty, value);
    }

    /// <summary>
    /// Gets or sets the margin to apply when this <see cref="Sheet"/> is undocked. The default value is
    /// <see cref="Thickness"/> with all sides set to 16.0.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Thickness UndockedMargin
    {
        get => (Thickness)GetValue(UndockedMarginProperty);
        set => SetValue(UndockedMarginProperty, value);
    }

    protected abstract double TargetSize { get; }

    protected abstract AnimationTimeline EnterAnimation { get; }

    protected abstract AnimationTimeline ExitAnimation { get; }

    internal abstract DependencyProperty TargetSizeProperty { get; }

    private SheetPresenter? currentHost;

    internal void SetCurrentHost(SheetPresenter host)
    {
        var overlay = host.GetOverlay();
        overlay.MouseLeftButtonDown += OnScrimOverlayMouseLeftButtonDown;

        currentHost = host;
    }

    internal void RemoveCurrentHost()
    {
        if (currentHost?.GetOverlay() is {} overlay)
        {
            overlay.MouseLeftButtonDown -= OnScrimOverlayMouseLeftButtonDown;
        }
        
        currentHost = null;
    }

    /// <summary>
    /// Invokes the <see cref="Opening"/> event.
    /// </summary>
    protected virtual void OnOpening() => RaiseEvent(new RoutedEventArgs(OpeningEvent, this));

    /// <summary>
    /// Invokes the <see cref="Closing"/> event.
    /// </summary>
    protected virtual void OnClosing() => RaiseEvent(new RoutedEventArgs(ClosingEvent, this));

    /// <summary>
    /// Invokes the <see cref="Opened"/> event.
    /// </summary>
    protected virtual void OnOpened() => RaiseEvent(new RoutedEventArgs(OpenedEvent, this));

    /// <summary>
    /// Invokes the <see cref="Closed"/> event.
    /// </summary>
    protected virtual void OnClosed() => RaiseEvent(new RoutedEventArgs(ClosedEvent, this));

    /// <summary>
    /// Invoked when the <see cref="IsOpen"/> property changes.
    /// </summary>
    protected virtual void OnIsOpenChanged(bool oldValue, bool newValue)
    {
        RaiseEvent(new RoutedPropertyChangedEventArgs<bool>(oldValue, newValue, IsOpenChangedEvent));

        UpdateOpenState(newValue);
    }

    /// <summary>
    /// Invoked when the <see cref="IsModal"/> property changes.
    /// </summary>
    protected virtual void OnIsModalChanged(bool oldValue, bool newValue)
    {
        RaiseEvent(new RoutedPropertyChangedEventArgs<bool>(oldValue, newValue, IsModalChangedEvent));

        if (isOpen)
        {
            UpdateModalState(newValue);
        }
    }

    /// <summary>
    /// Invoked when the <see cref="IsDocked"/> property changes.
    /// </summary>
    protected virtual void OnIsDockedChanged(bool oldValue, bool newValue)
    {
        RaiseEvent(new RoutedPropertyChangedEventArgs<bool>(oldValue, newValue, IsDockedChangedEvent));

        UpdateDockedState(newValue);
    }

    private static void OnIsOpenChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((Sheet)element).OnIsOpenChanged((bool)e.OldValue, (bool)e.NewValue);

    private static void OnIsModalChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((Sheet)element).OnIsModalChanged((bool)e.OldValue, (bool)e.NewValue);

    private static void OnIsDockedChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((Sheet)element).OnIsDockedChanged((bool)e.OldValue, (bool)e.NewValue);

    protected static DoubleAnimation CreateBaseAnimation(
        double desiredValue,
        int desiredFrameRate = 60,
        bool freeze = true)
    {
        var animation = new DoubleAnimation
        {
            To = desiredValue,
            Duration = MotionDurations.Medium400,
            EasingFunction = MotionEasings.Emphasized
        };

        Timeline.SetDesiredFrameRate(animation, desiredFrameRate);

        if (freeze)
        {
            animation.Freeze();
        }

        return animation;
    }

    private void OnScrimOverlayMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (isOpen)
        {
             SetCurrentValue(IsOpenProperty, false);
        }
    }
    
    private void UpdateModalState(bool setIsModal, bool isAnimate = true)
    {
        if (isModal == setIsModal)
        {
            return;
        }

        var overlay = currentHost?.GetOverlay();
        if (overlay is null)
        {
            return;
        }

        isModal = setIsModal;
        
        overlay.IsHitTestVisible = setIsModal;

        if (setIsModal)
        {
            if (isAnimate)
            {
                overlay.BeginAnimation(OpacityProperty, enterScrimAnimation);
            }
            else
            {
                overlay.Opacity = ScrimOverlayOpacity;
            }
        }
        else
        {
            if (isAnimate)
            {
                overlay.BeginAnimation(OpacityProperty, exitScrimAnimation);
            }
            else
            {
                overlay.Opacity = 0.0;
            }
        }
    }

    private void UpdateDockedState(bool setIsDocked)
    {
        if (isDocked == setIsDocked)
        {
            return;
        }

        var container = currentHost?.GetContainer();
        if (container is null)
        {
            return;
        }

        isDocked = setIsDocked;

        if (setIsDocked)
        {
            container.ClearValue(MarginProperty);
        }
        else
        {
            container.SetBinding(MarginProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(UndockedMarginProperty)
            });
        }
    }

    private void UpdateOpenState(bool setIsOpen, bool isAnimate = true)
    {
        if (isOpen == setIsOpen)
        {
            return;
        }

        var container = currentHost?.GetContainer();
        if (container is null || !IsLoaded)
        {
            return;
        }

        isOpen = setIsOpen;

        if (IsModal)
        {
            // We also need to update the modal state
            UpdateModalState(setIsOpen, isAnimate);
        }

        if (setIsOpen)
        {
            OnOpening();

            if (isAnimate)
            {
                container.BeginAnimation(TargetSizeProperty, EnterAnimation);
            }
            else
            {
                container.SetValue(TargetSizeProperty, TargetSize);

                OnOpened();
            }
        }
        else
        {
            OnClosing();

            if (isAnimate)
            {
                container.BeginAnimation(TargetSizeProperty, ExitAnimation);
            }
            else
            {
                container.SetValue(TargetSizeProperty, 0.0);

                OnClosed();
            }
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        UpdateDockedState(IsDocked);
        UpdateOpenState(IsOpen, isAnimate: false);
    }
}