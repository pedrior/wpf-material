using System.Windows.Media.Animation;

namespace WPF.Material.Components;

/// <summary>
/// Represents a control that behaves like a <see cref="Button"/>, but is intended to float above the content to promote
/// the most common or important action on a screen, such as creating a new item, composing some content, or starting a
/// primary task.
/// </summary>
[TemplatePart(Name = PartContent, Type = typeof(FrameworkElement))]
public class FloatingActionButton : System.Windows.Controls.Button
{
    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type),
        typeof(FloatingActionButtonType),
        typeof(FloatingActionButton),
        new PropertyMetadata(FloatingActionButtonType.Surface));

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        nameof(Size),
        typeof(FloatingActionButtonSize),
        typeof(FloatingActionButton),
        new PropertyMetadata(FloatingActionButtonSize.Standard));

    /// <summary>
    /// Identifies the <see cref="CanExtend"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CanExtendProperty = DependencyProperty.Register(
        nameof(CanExtend),
        typeof(bool),
        typeof(FloatingActionButton),
        new PropertyMetadata(false, OnCanExtendChanged));

    /// <summary>
    /// Identifies the <see cref="IsExtended"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsExtendedProperty = DependencyProperty.Register(
        nameof(IsExtended),
        typeof(bool),
        typeof(FloatingActionButton),
        new PropertyMetadata(false, OnIsExtendedChanged));

    private const string PartContent = "PART_Content";

    private static readonly Duration AnimationDuration = AnimationDurations.Short200;
    private static readonly IEasingFunction AnimationEasingFunction = AnimationEasings.StandardDecelerated;

    private Storyboard? extendStoryboard;
    private Storyboard? shrinkStoryboard;

    private FrameworkElement? content;

    private bool isExtended;
    private bool shouldExtendAfterShrink;

    static FloatingActionButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(FloatingActionButton),
            new FrameworkPropertyMetadata(typeof(FloatingActionButton)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FloatingActionButton"/> class.
    /// </summary>
    public FloatingActionButton()
    {
        Unloaded += OnUnloaded;
    }

    /// <summary>
    /// Gets or sets the type of the FAB, which determines its appearance and emphasis. The default value is
    /// <see cref="FloatingActionButtonType.Surface"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public FloatingActionButtonType Type
    {
        get => (FloatingActionButtonType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the FAB. The default value is <see cref="FloatingActionButtonSize.Standard"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public FloatingActionButtonSize Size
    {
        get => (FloatingActionButtonSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the FAB can be extended with a label. The default value is
    /// <see langword="false"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public bool CanExtend
    {
        get => (bool)GetValue(CanExtendProperty);
        set => SetValue(CanExtendProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the FAB is extended with a label. The default value is
    /// <see langword="false"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public bool IsExtended
    {
        get => (bool)GetValue(IsExtendedProperty);
        set => SetValue(IsExtendedProperty, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (content is not null)
        {
            content.Loaded -= OnContentLoaded;
        }

        content = GetTemplateChild(PartContent) as FrameworkElement
                  ?? throw new NullReferenceException($"Missing required template part '{PartContent}'.");

        content.Loaded += OnContentLoaded;
    }

    protected override void OnContentChanged(object oldContent, object newContent)
    {
        if (isExtended)
        {
            shouldExtendAfterShrink = true;
            Shrink();
        }

        base.OnContentChanged(oldContent, newContent);
    }

    private void OnContentLoaded(object sender, RoutedEventArgs e) => InvalidateExtend(animateExtend: false);

    private static void OnCanExtendChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((FloatingActionButton)element).InvalidateExtend();

    private static void OnIsExtendedChanged(DependencyObject element, DependencyPropertyChangedEventArgs e) =>
        ((FloatingActionButton)element).InvalidateExtend();

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= OnUnloaded;

        if (shrinkStoryboard is not null)
        {
            shrinkStoryboard.Completed -= OnShrinkStoryboardCompleted;
        }
    }

    private void InvalidateExtend(bool animateExtend = true)
    {
        if (content is null)
        {
            return;
        }

        if (CanExtend)
        {
            if (IsExtended && !isExtended)
            {
                Extend(animateExtend);
            }
            else if (isExtended)
            {
                Shrink();
            }
        }
        else if (isExtended)
        {
            Shrink();
        }
    }

    private void Extend(bool animateExtend)
    {
        isExtended = true;

        var to = ComputeDesiredExtendedWidth();
        if (!animateExtend)
        {
            Width = to;
            return;
        }

        var from = double.IsNaN(Width) ? ActualWidth : Width;

        CreateOrUpdateExtendStoryboard(to, from);

        extendStoryboard!.Begin();
    }

    private void Shrink()
    {
        isExtended = false;

        CreateOrUpdateShrinkStoryboard(MinWidth);

        shrinkStoryboard!.Begin();
    }

    private void CreateOrUpdateExtendStoryboard(double toWidth, double fromWidth)
    {
        if (extendStoryboard is null)
        {
            extendStoryboard = CreateExtendStoryboard(toWidth, fromWidth);
        }
        else
        {
            var widthAnimation = (DoubleAnimation)extendStoryboard.Children[0];

            widthAnimation.To = toWidth;
            widthAnimation.From = fromWidth;
        }
    }

    private void CreateOrUpdateShrinkStoryboard(double toWidth)
    {
        if (shrinkStoryboard is null)
        {
            shrinkStoryboard = CreateShrinkStoryboard(toWidth);
        }
        else
        {
            var widthAnimation = (DoubleAnimation)shrinkStoryboard.Children[0];

            widthAnimation.To = toWidth;
        }
    }

    private Storyboard CreateExtendStoryboard(double toWidth, double fromWidth)
    {
        var widthAnimation = new DoubleAnimation
        {
            To = toWidth,
            From = fromWidth,
            Duration = AnimationDuration,
            EasingFunction = AnimationEasingFunction
        };

        var opacityAnimation = new DoubleAnimation
        {
            To = 1.0,
            Duration = AnimationDuration,
            EasingFunction = AnimationEasingFunction
        };

        Storyboard.SetTarget(widthAnimation, this);
        Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(WidthProperty));

        Storyboard.SetTarget(opacityAnimation, content);
        Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));

        return new Storyboard
        {
            Children =
            {
                widthAnimation,
                opacityAnimation
            }
        };
    }

    private Storyboard CreateShrinkStoryboard(double toWidth)
    {
        var widthAnimation = new DoubleAnimation
        {
            To = toWidth,
            Duration = AnimationDuration,
            EasingFunction = AnimationEasingFunction
        };

        var opacityAnimation = new DoubleAnimation
        {
            To = 0.0,
            Duration = AnimationDuration,
            EasingFunction = AnimationEasingFunction
        };

        Storyboard.SetTarget(widthAnimation, this);
        Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(WidthProperty));

        Storyboard.SetTarget(opacityAnimation, content);
        Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));

        var storyboard = new Storyboard
        {
            Children =
            {
                widthAnimation,
                opacityAnimation
            }
        };

        storyboard.Completed += OnShrinkStoryboardCompleted;

        return storyboard;
    }

    private void OnShrinkStoryboardCompleted(object? sender, EventArgs e)
    {
        if (!shouldExtendAfterShrink)
        {
            return;
        }

        shouldExtendAfterShrink = false;
        Extend(true);
    }

    private double ComputeDesiredExtendedWidth()
    {
        var minWidth = MinWidth;
        if (content is null)
        {
            return minWidth;
        }
        
        content!.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        return content.DesiredSize.Width + minWidth + Layout.GetSpacing(this);
    }
}