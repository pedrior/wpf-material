using System.Windows.Media.Animation;

namespace WPF.Material.Components;

/// <summary>
/// Provides a base class for sheet controls>. A <see cref="Sheet"/> acts like a<see cref="ContentControl"/> that
/// slides in and out of the screen.
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
        new PropertyMetadata(new Thickness()));

    /// <summary>
    /// Identifies the <see cref="IsOpenChanged"/> routed event.
    /// </summary>
    public static readonly RoutedEvent IsOpenChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(IsOpenChanged),
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

    private static readonly Duration AnimationDuration = TimeSpan.FromSeconds(0.3);

    private bool isOpen;
    private bool isDocked = true;

    /// <summary>
    /// Occurs when the <see cref="IsOpen"/> property changes.
    /// </summary>
    public event RoutedPropertyChangedEventHandler<bool> IsOpenChanged
    {
        add => AddHandler(IsOpenChangedEvent, value);
        remove => RemoveHandler(IsOpenChangedEvent, value);
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
    /// Initializes a new instance of <see cref="Sheet"/>.
    /// </summary>
    protected Sheet()
    {
        Loaded += OnLoaded;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="Sheet"/> is open.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="Sheet"/> is docked to its parent. When undocked, the
    /// margin defined by the <see cref="UndockedMargin"/> property is applied.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsDocked
    {
        get => (bool)GetValue(IsDockedProperty);
        set => SetValue(IsDockedProperty, value);
    }

    /// <summary>
    /// Gets or sets the margin to apply when this <see cref="Sheet"/> is undocked.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public Thickness UndockedMargin
    {
        get => (Thickness)GetValue(UndockedMarginProperty);
        set => SetValue(UndockedMarginProperty, value);
    }

    protected abstract double TargetSize { get; }

    protected abstract AnimationTimeline OpenAnimation { get; }

    protected abstract AnimationTimeline CloseAnimation { get; }

    internal abstract DependencyProperty TargetSizeProperty { get; }

    private SheetPresenter? currentHost;

    internal void SetCurrentHost(SheetPresenter host) => currentHost = host;

    internal void RemoveCurrentHost() => currentHost = null;

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

        if (currentHost?.GetContainer() is { } container)
        {
            UpdateOpenState(container, newValue);
        }
    }

    /// <summary>
    /// Invoked when the <see cref="IsDocked"/> property changes.
    /// </summary>
    protected virtual void OnIsDockedChanged(bool oldValue, bool newValue)
    {
        RaiseEvent(new RoutedPropertyChangedEventArgs<bool>(oldValue, newValue, IsDockedChangedEvent));

        if (currentHost?.GetContainer() is { } container)
        {
            UpdateDockedState(container, newValue);
        }
    }

    private static void OnIsOpenChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((Sheet)element).OnIsOpenChanged((bool)e.OldValue, (bool)e.NewValue);

    private static void OnIsDockedChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((Sheet)element).OnIsDockedChanged((bool)e.OldValue, (bool)e.NewValue);

    protected static DoubleAnimationUsingKeyFrames CreateBaseAnimation(
        double desiredValue,
        int desiredFrameRate = 60,
        bool freeze = true)
    {
        var animation = new DoubleAnimationUsingKeyFrames
        {
            Duration = AnimationDuration,
            KeyFrames =
            {
                // Like an ease-in-out animation curve, but with a bit more ease at the beginning.
                // The https://cubic-bezier.com/#.6,0,.2,1 is a good tool to visualize the curve.
                new SplineDoubleKeyFrame(desiredValue, KeyTime.Paced)
                {
                    KeySpline = new KeySpline
                    {
                        ControlPoint1 = new Point(0.6, 0.0),
                        ControlPoint2 = new Point(0.2, 1.0)
                    }
                }
            }
        };

        Timeline.SetDesiredFrameRate(animation, desiredFrameRate);

        if (freeze)
        {
            animation.Freeze();
        }

        return animation;
    }

    private void UpdateDockedState(Container container, bool state)
    {
        if (isDocked == state)
        {
            return;
        }

        if (state)
        {
            container.ClearValue(MarginProperty);
            isDocked = true;
        }
        else
        {
            container.SetBinding(MarginProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(UndockedMarginProperty)
            });

            isDocked = false;
        }
    }

    private void UpdateOpenState(Container container, bool state, bool isAnimate = true)
    {
        if (!IsLoaded || isOpen == state)
        {
            return;
        }

        if (state)
        {
            OnOpening();

            if (isAnimate)
            {
                container.BeginAnimation(TargetSizeProperty, OpenAnimation);
            }
            else
            {
                container.SetValue(TargetSizeProperty, TargetSize);

                OnOpened();
            }

            isOpen = true;
        }
        else
        {
            OnClosing();

            if (isAnimate)
            {
                container.BeginAnimation(TargetSizeProperty, CloseAnimation);
            }
            else
            {
                container.SetValue(TargetSizeProperty, 0.0);

                OnClosed();
            }

            isOpen = false;
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        if (currentHost?.GetContainer() is not { } container)
        {
            return;
        }

        UpdateDockedState(container, IsDocked);
        UpdateOpenState(container, IsOpen, isAnimate: false);
    }
}