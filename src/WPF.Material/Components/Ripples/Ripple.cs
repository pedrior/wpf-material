using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WPF.Material.Components;

/// <summary>
/// Provides a ripple effect over a content when the user interacts with it.
/// </summary>
[TemplatePart(Name = PartEllipse, Type = typeof(Ellipse))]
public class Ripple : ContentControl
{
    /// <summary>
    /// Identifies the <see cref="TriggerMode"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TriggerModeProperty = DependencyProperty.Register(
        nameof(TriggerMode),
        typeof(RippleTriggerMode),
        typeof(Ripple),
        new PropertyMetadata(RippleTriggerMode.MousePress));

    /// <summary>
    /// Identifies the <see cref="ReleaseMode"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ReleaseModeProperty = DependencyProperty.Register(
        nameof(ReleaseMode),
        typeof(RippleReleaseMode),
        typeof(Ripple),
        new PropertyMetadata(RippleReleaseMode.Auto));

    /// <summary>
    /// Identifies the <see cref="IsAnimated"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsAnimatedProperty = DependencyProperty.Register(
        nameof(IsAnimated),
        typeof(bool),
        typeof(Ripple),
        new PropertyMetadata(true, IsAnimatedChanged));
    
    /// <summary>
    /// Identifies the <see cref="IsCentered"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsCenteredProperty = DependencyProperty.Register(
        nameof(IsCentered),
        typeof(bool),
        typeof(Ripple),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsUnbounded"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsUnboundedProperty = DependencyProperty.Register(
        nameof(IsUnbounded),
        typeof(bool),
        typeof(Ripple),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="RippleOutline"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RippleOutlineProperty = DependencyProperty.Register(
        nameof(RippleOutline),
        typeof(Geometry),
        typeof(Ripple),
        new PropertyMetadata(null));

    private const int AnimationFrameRate = 60;
    private const double EllipseOpacity = 0.1;

    private const string PartEllipse = "PART_Ellipse";

    private static readonly Duration AnimationDuration = TimeSpan.FromSeconds(0.3);

    private static readonly PropertyPath AnimationOpacityPropertyPath = new(OpacityProperty);
    private static readonly PropertyPath AnimationScaleXPropertyPath = new($"{ScaleTransform.ScaleXProperty.Name}");
    private static readonly PropertyPath AnimationScaleYPropertyPath = new($"{ScaleTransform.ScaleYProperty.Name}");

    private Ellipse ellipse = null!;

    private Storyboard? startStoryboard;
    private Storyboard? releaseStoryboard;

    private bool isMouseDown;
    private bool isPendingRelease;
    private bool forceAutoRelease;

    // Used to store the mouse press point for later use upon mouse release when TriggerMode is MouseRelease.
    private Point? pressPoint;

    static Ripple()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Ripple),
            new FrameworkPropertyMetadata(typeof(Ripple)));

        SnapsToDevicePixelsProperty.OverrideMetadata(
            typeof(Ripple),
            new FrameworkPropertyMetadata(true));
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Ripple"/>.
    /// </summary>
    public Ripple()
    {
        EventManager.RegisterClassHandler(
            typeof(Control),
            PreviewMouseUpEvent,
            new MouseButtonEventHandler(HandlePreviewMouseUpEvent));
    }

    /// <summary>
    /// Gets or sets the trigger mode that determines when the ripple effect should be started. The default value is
    /// <see cref="RippleTriggerMode.MousePress"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public RippleTriggerMode TriggerMode
    {
        get => (RippleTriggerMode)GetValue(TriggerModeProperty);
        set => SetValue(TriggerModeProperty, value);
    }

    /// <summary>
    /// Gets or sets the release mode that determines when the ripple effect should be released. The default value is
    /// <see cref="RippleReleaseMode.Auto"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public RippleReleaseMode ReleaseMode
    {
        get => (RippleReleaseMode)GetValue(ReleaseModeProperty);
        set => SetValue(ReleaseModeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a value indicating whether the ripple effect should be animated. If this property is
    /// <see langword="false"/>, the ripple effect will not be shown. The default value is <see langword="true"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsAnimated
    {
        get => (bool)GetValue(IsAnimatedProperty);
        set => SetValue(IsAnimatedProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ripple effect should be centered on this element; otherwise, it
    /// will be started at the mouse position relative to this element. The default value is <see langword="false"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsCentered
    {
        get => (bool)GetValue(IsCenteredProperty);
        set => SetValue(IsCenteredProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ripple effect should be unbounded. If this property is set to
    /// <see langword="false"/>, the ripple effect will be bounded to the <see cref="RippleOutline"/> geometry of this
    /// element. The default value is <see langword="false"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsUnbounded
    {
        get => (bool)GetValue(IsUnboundedProperty);
        set => SetValue(IsUnboundedProperty, value);
    }

    /// <summary>
    /// Gets or sets a <see cref="Geometry"/> that defines the outline geometry of the ripple effect. If this property
    /// is set, the ripple effect will be bounded to this geometry. The default value is <see langword="null"/>.
    /// </summary>
    public Geometry? RippleOutline
    {
        get => (Geometry?)GetValue(RippleOutlineProperty);
        set => SetValue(RippleOutlineProperty, value);
    }

    /// <summary>
    /// Starts the ripple effect at the specified origin point for manual trigger mode.
    /// </summary>
    /// <param name="rippleOrigin">
    /// The point where the ripple effect should start. The <paramref name="rippleOrigin"/>
    /// is always set to the nearest edge if it is outside the boundaries of this element. Based on this, it is
    /// important that the <paramref name="rippleOrigin"/> should always be relative to this element; otherwise, the
    /// origin point may not be accurate.
    /// </param>
    public void Start(Point rippleOrigin)
    {
        if (!IsAnimated || TriggerMode is not RippleTriggerMode.Manual)
        {
            return;
        }
        
        rippleOrigin.X = Math.Clamp(rippleOrigin.X, 0.0, ActualWidth);
        rippleOrigin.Y = Math.Clamp(rippleOrigin.Y, 0.0, ActualHeight);

        StartInternal(rippleOrigin);
    }

    /// <summary>
    /// Releases the ripple effect for manual release mode. If this element is pending release, the ripple effect will
    /// be released immediately; otherwise, it will be released after the start animation is finished.
    /// </summary>
    public void Release()
    {
        if (ReleaseMode is not RippleReleaseMode.Manual)
        {
            return;
        }

        if (isPendingRelease)
        {
            ReleaseInternal();
        }
        else
        {
            forceAutoRelease = true;
        }
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ellipse = GetTemplateChild(PartEllipse) as Ellipse
                  ?? throw new NullReferenceException($"Missing template part '{PartEllipse}'.");

        // Prepare for transformation
        ellipse.RenderTransformOrigin = new Point(0.5, 0.5);
        ellipse.RenderTransform = new ScaleTransform(0.0, 0.0);

        if (IsAnimated)
        {
            CreateRippleAnimations();
        }
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        if (!IsAnimated || TriggerMode is RippleTriggerMode.Manual)
        {
            return;
        }

        isMouseDown = true;

        var rippleOrigin = GetRippleOrigin();
        
        switch (TriggerMode)
        {
            case RippleTriggerMode.MousePress:
                StartInternal(rippleOrigin);
                break;
            case RippleTriggerMode.MouseRelease:
                pressPoint = rippleOrigin;
                break;
        }
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        base.OnMouseLeave(e);

        if (isPendingRelease && ReleaseMode is RippleReleaseMode.Auto)
        {
            ReleaseInternal();
        }
    }
    
    private static void IsAnimatedChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var ripple = (Ripple)element;
        if ((bool)e.NewValue)
        {
            ripple.CreateRippleAnimations();
        }
        else
        {
            ripple.DestroyRippleAnimations();
        }
    }

    private void HandlePreviewMouseUpEvent(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton is not MouseButton.Left || ReleaseMode is not RippleReleaseMode.Auto)
        {
            return;
        }

        isMouseDown = false;

        if (isPendingRelease)
        {
            ReleaseInternal();
        }
        else if (pressPoint is not null)
        {
            StartInternal(pressPoint.Value);
        }
    }

    private Point GetRippleOrigin()
    {
        return IsCentered
            ? new Point(ActualWidth * 0.5, ActualHeight * 0.5)
            : Mouse.GetPosition(this);
    }

    private void StartInternal(Point rippleOrigin)
    {
        pressPoint = null;
        
        var rippleSize = GetDesiredRippleSize(rippleOrigin);
        UpdateRipplePositionAndSize(rippleOrigin, rippleSize);

        BeginStartAnimation();
    }

    private void ReleaseInternal()
    {
        isMouseDown = false;
        isPendingRelease = false;
        forceAutoRelease = false;

        BeginReleaseAnimation();
    }

    private double GetDesiredRippleSize(Point rippleOrigin)
    {
        const double multiplier = 3.0;

        var x = Math.Max(rippleOrigin.X, ActualWidth - rippleOrigin.X);
        var y = Math.Max(rippleOrigin.Y, ActualHeight - rippleOrigin.Y);

        return Math.Sqrt(x * x + y * y) * multiplier;
    }

    private void UpdateRipplePositionAndSize(Point rippleOrigin, double rippleSize)
    {
        ellipse.Width = rippleSize;
        ellipse.Height = rippleSize;

        Canvas.SetTop(ellipse, rippleOrigin.Y - rippleSize * 0.5);
        Canvas.SetLeft(ellipse, rippleOrigin.X - rippleSize * 0.5);
    }

    private void CreateRippleAnimations()
    {
        CreateStartAnimation();
        CreateReleaseAnimation();
    }
    
    private void DestroyRippleAnimations()
    {
        startStoryboard?.Stop();
        releaseStoryboard?.Stop();

        startStoryboard = null;
        releaseStoryboard = null;
    }

    private void CreateStartAnimation()
    {
        var scaleXAnimation = new DoubleAnimation
        {
            To = 1.0,
            From = 0.1,
            Duration = AnimationDuration
        };

        var scaleYAnimation = scaleXAnimation.Clone();

        var opacityAnimation = new DoubleAnimation
        {
            To = EllipseOpacity,
            From = 0.05,
            Duration = AnimationDuration
        };

        startStoryboard = new Storyboard
        {
            Children =
            {
                scaleXAnimation,
                scaleYAnimation,
                opacityAnimation
            }
        };

        Timeline.SetDesiredFrameRate(startStoryboard, AnimationFrameRate);

        Storyboard.SetTarget(scaleXAnimation, ellipse.RenderTransform);
        Storyboard.SetTarget(scaleYAnimation, ellipse.RenderTransform);
        Storyboard.SetTarget(opacityAnimation, ellipse);

        Storyboard.SetTargetProperty(scaleXAnimation, AnimationScaleXPropertyPath);
        Storyboard.SetTargetProperty(scaleYAnimation, AnimationScaleYPropertyPath);
        Storyboard.SetTargetProperty(opacityAnimation, AnimationOpacityPropertyPath);

        startStoryboard.Completed += OnStartAnimationCompleted;

        startStoryboard.Freeze();
    }

    private void CreateReleaseAnimation()
    {
        var opacityAnimation = new DoubleAnimation
        {
            To = 0.0,
            Duration = AnimationDuration
        };

        releaseStoryboard = new Storyboard
        {
            Children = { opacityAnimation }
        };

        Timeline.SetDesiredFrameRate(releaseStoryboard, AnimationFrameRate);

        Storyboard.SetTarget(opacityAnimation, ellipse);

        Storyboard.SetTargetProperty(opacityAnimation, AnimationOpacityPropertyPath);

        releaseStoryboard.Freeze();
    }

    private void BeginStartAnimation() => startStoryboard?.Begin(ellipse);

    private void BeginReleaseAnimation() => releaseStoryboard?.Begin(ellipse);

    private void OnStartAnimationCompleted(object? sender, EventArgs e)
    {
        if (!isMouseDown && (ReleaseMode is RippleReleaseMode.Auto || forceAutoRelease))
        {
            ReleaseInternal();
        }
        else
        {
            isPendingRelease = true;
        }
    }
}