using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WPF.Material.Foundations;

namespace WPF.Material.Components;

/// <summary>
/// Represents a <see cref="ContentControl"/> that provides a ripple effect over its content when clicked.
/// </summary>
[TemplatePart(Name = PartEllipse, Type = typeof(Ellipse))]
[EditorBrowsable(EditorBrowsableState.Never)]
public class Ripple : ContentControl
{
    /// <summary>
    /// Identifies the FrameRate attached property.
    /// </summary>
    public static readonly DependencyProperty FrameRateProperty = DependencyProperty.RegisterAttached(
        "FrameRate",
        typeof(int?),
        typeof(Ripple),
        new FrameworkPropertyMetadata(
            60,
            FrameworkPropertyMetadataOptions.Inherits,
            propertyChangedCallback: null,
            coerceValueCallback: CoerceFrameRate));

    /// <summary>
    /// Identifies the <see cref="ClippingGeometry"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ClippingGeometryProperty = DependencyProperty.Register(
        nameof(ClippingGeometry),
        typeof(Geometry),
        typeof(Ripple),
        new PropertyMetadata(null));

    private const string PartEllipse = "PART_Ellipse";

    private static readonly TimeSpan ZeroDuration = TimeSpan.Zero;

    private static readonly TimeSpan AnimationDuration100 = TimeSpan.FromMilliseconds(100);
    private static readonly TimeSpan AnimationDuration300 = TimeSpan.FromMilliseconds(300);

    private static readonly PropertyPath EllipseOpacityPropertyPath = new("(UIElement.Opacity)");

    private static readonly PropertyPath EllipseScaleXPropertyPath = new(
        "(UIElement.RenderTransform).(ScaleTransform.ScaleX)");

    private static readonly PropertyPath EllipseScaleYPropertyPath = new(
        "(UIElement.RenderTransform).(ScaleTransform.ScaleY)");

    private Ellipse ellipse = null!;

    private Storyboard? enterAnimation;
    private Storyboard? exitAnimation;
    
    private bool isPressed;
    private bool isWaitingForExit;
    
    private bool keepState;
    
    static Ripple()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Ripple),
            new FrameworkPropertyMetadata(typeof(Ripple)));
        
        Interaction.IsRippleEnabledProperty.OverrideMetadata(
            typeof(Ripple),
            new FrameworkPropertyMetadata(
                defaultValue: true,
                propertyChangedCallback: IsRippleEnabledAttachedPropertyChanged));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ripple"/> class.
    /// </summary>
    public Ripple()
    {
        EventManager.RegisterClassHandler(
            typeof(ContentControl),
            Mouse.MouseUpEvent,
            new MouseButtonEventHandler(MouseUpHandler));
    }

    /// <summary>
    /// Gets or sets the <see cref="Geometry"/> used to clip the ripple effect.
    /// </summary>
    [Bindable(false)]
    public Geometry? ClippingGeometry
    {
        get => (Geometry?)GetValue(ClippingGeometryProperty);
        set => SetValue(ClippingGeometryProperty, value);
    }

    /// <summary>
    /// Sets the value of the <see cref="FrameRateProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="FrameRateProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetFrameRate(DependencyObject element, int? value) => element.SetValue(FrameRateProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="FrameRateProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="FrameRateProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="FrameRateProperty"/> attached property on the specified dependency object.
    /// </returns>
    public static int? GetFrameRate(DependencyObject element) => (int?)element.GetValue(FrameRateProperty);

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ellipse = GetTemplateChild(PartEllipse) as Ellipse
                  ?? throw new InvalidOperationException($"Missing required template part: {PartEllipse}");
        
        ellipse.RenderTransformOrigin = new Point(0.5, 0.5);
        ellipse.RenderTransform = new ScaleTransform(0.0, 0.0);

        if (Interaction.GetIsRippleEnabled(this))
        {
            CreateAnimations();
        }
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        e.Handled = false;
        isPressed = true;

        Start();
    }

    internal void Start(bool keep = false)
    {
        if (!Interaction.GetIsRippleEnabled(this))
        {
            return;
        }

        keepState = keep;

        var origin = Interaction.GetIsRippleCentered(this)
            ? new Point(ActualWidth * 0.5, ActualHeight * 0.5)
            : Mouse.GetPosition(this);

        var size = CalculateEllipseFinalSize(origin);

        SetEllipseSize(size);
        SetEllipsePosition(origin, size);

        BeginEnterAnimation();
    }

    internal void Release()
    {
        if (!keepState)
        {
            return;
        }

        isPressed = false;
        keepState = false;

        if (isWaitingForExit)
        {
            BeginExitAnimation();
        }
    }

    private void MouseUpHandler(object sender, MouseEventArgs e)
    {
        if (keepState)
        {
            return;
        }

        isPressed = false;
        if (isWaitingForExit)
        {
            BeginExitAnimation();
        }
    }

    private void SetEllipseSize(double size)
    {
        ellipse.Width = size;
        ellipse.Height = size;
    }

    private double CalculateEllipseFinalSize(Point origin)
    {
        var x = Math.Max(origin.X, ActualWidth - origin.X);
        var y = Math.Max(origin.Y, ActualHeight - origin.Y);
        
        return Math.Sqrt(x * x + y * y) * 3.0;
    }

    private void SetEllipsePosition(Point origin, double size)
    {
        var halfSize = size * 0.5;

        Canvas.SetLeft(ellipse, origin.X - halfSize);
        Canvas.SetTop(ellipse, origin.Y - halfSize);
    }

    private void CreateAnimations()
    {
        var desiredFrameRate = GetFrameRate(this);

        CreateEnterAnimation(desiredFrameRate);
        CreateExitAnimation(desiredFrameRate);
    }

    private void DestroyAnimations()
    {
        if (enterAnimation is not null)
        {
            enterAnimation.Completed -= EnterAnimationCompleted;
            enterAnimation.Stop(ellipse);
        }

        if (exitAnimation is not null)
        {
            exitAnimation.Completed -= ExitAnimationCompleted;
            exitAnimation.Stop(ellipse);
        }

        enterAnimation = null;
        exitAnimation = null;
    }

    private void CreateEnterAnimation(int? desiredFrameRate)
    {
        var scaleKeyFrame1 = new EasingDoubleKeyFrame(0.2, ZeroDuration);
        var scaleKeyFrame2 = new EasingDoubleKeyFrame(0.4, AnimationDuration100);
        var scaleKeyFrame3 = new EasingDoubleKeyFrame(1.0, AnimationDuration300);

        var scaleXAnimation = new DoubleAnimationUsingKeyFrames
        {
            KeyFrames =
            {
                scaleKeyFrame1,
                scaleKeyFrame2,
                scaleKeyFrame3
            }
        };

        var scaleYAnimation = new DoubleAnimationUsingKeyFrames
        {
            KeyFrames =
            {
                scaleKeyFrame1,
                scaleKeyFrame2,
                scaleKeyFrame3
            }
        };

        var opacityAnimation = new DoubleAnimationUsingKeyFrames
        {
            KeyFrames =
            {
                new EasingDoubleKeyFrame(0.1, ZeroDuration)
                // new EasingDoubleKeyFrame(0.01, AnimationDuration100),
                // new EasingDoubleKeyFrame(0.06, AnimationDuration100),
                // new EasingDoubleKeyFrame(0.14, AnimationDuration300)
            }
        };

        Storyboard.SetTargetProperty(scaleXAnimation, EllipseScaleXPropertyPath);
        Storyboard.SetTargetProperty(scaleYAnimation, EllipseScaleYPropertyPath);
        Storyboard.SetTargetProperty(opacityAnimation, EllipseOpacityPropertyPath);

        enterAnimation = new Storyboard
        {
            Children =
            {
                scaleXAnimation,
                scaleYAnimation,
                opacityAnimation
            }
        };

        Timeline.SetDesiredFrameRate(enterAnimation, desiredFrameRate);

        enterAnimation!.Completed += EnterAnimationCompleted;

        enterAnimation.Freeze();
    }

    private void CreateExitAnimation(int? desiredFrameRate)
    {
        var scaleKeyFrame = new EasingDoubleKeyFrame(1.0, ZeroDuration);
        var scaleXAnimation = new DoubleAnimationUsingKeyFrames { KeyFrames = { scaleKeyFrame } };
        var scaleYAnimation = new DoubleAnimationUsingKeyFrames { KeyFrames = { scaleKeyFrame } };
        var opacityAnimation = new DoubleAnimationUsingKeyFrames
        {
            KeyFrames =
            {
                new EasingDoubleKeyFrame(0.0, AnimationDuration300)
            }
        };

        Storyboard.SetTargetProperty(scaleXAnimation, EllipseScaleXPropertyPath);
        Storyboard.SetTargetProperty(scaleYAnimation, EllipseScaleYPropertyPath);
        Storyboard.SetTargetProperty(opacityAnimation, EllipseOpacityPropertyPath);

        exitAnimation = new Storyboard
        {
            Children =
            {
                scaleXAnimation,
                scaleYAnimation,
                opacityAnimation
            }
        };

        Timeline.SetDesiredFrameRate(exitAnimation, desiredFrameRate);

        exitAnimation.Completed += ExitAnimationCompleted;

        exitAnimation.Freeze();
    }

    private void EnterAnimationCompleted(object? sender, EventArgs e)
    {
        if (isPressed || keepState)
        {
            isWaitingForExit = true;
        }
        else
        {
            BeginExitAnimation();
        }
    }

    private void ExitAnimationCompleted(object? sender, EventArgs e) => isWaitingForExit = false;

    private void BeginEnterAnimation() => BeginAnimation(enterAnimation);

    private void BeginExitAnimation() => BeginAnimation(exitAnimation);

    private void BeginAnimation(Storyboard? animation)
    {
        StopAnimation(enterAnimation); // Avoid flickering

        animation?.Begin(
            ellipse,
            HandoffBehavior.SnapshotAndReplace,
            isControllable: true);
    }

    private void StopAnimation(Storyboard? animation) => animation?.Stop(ellipse);

    private static void IsRippleEnabledAttachedPropertyChanged(
        DependencyObject element,
        DependencyPropertyChangedEventArgs e)
    {
        if (element is not Ripple ripple)
        {
            return;
        }

        if ((bool)e.NewValue)
        {
            ripple.CreateAnimations();
        }
        else
        {
            ripple.DestroyAnimations();
        }
    }

    private static object? CoerceFrameRate(DependencyObject element, object? value) =>
        value is int rate ? Math.Clamp(rate, 1, 60) : value;
}