using System.Windows.Input;
using System.Windows.Markup;

namespace WPF.Material.Components;

/// <summary>
/// Represents a control that draws an indicator around a focused element when it has keyboard focus.
/// </summary>
/// <remarks>
/// A UI element can receive keyboard focus by being navigated with a key or by being clicked with the mouse. That
/// said, the focus indicator is restricted to the keyboard device, i.e. no indicator is drawn when the device that
/// caused the element to be focused on is not the keyboard.
/// </remarks>
[ContentProperty(nameof(Child))]
[EditorBrowsable(EditorBrowsableState.Never)]
public class KeyboardFocusIndicator : FrameworkElement
{
    /// <summary>
    /// Identifies the <see cref="Brush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
        nameof(Brush),
        typeof(Brush),
        typeof(KeyboardFocusIndicator),
        new PropertyMetadata(Brushes.Black, (o, _) => ((KeyboardFocusIndicator)o).TryRedrawIndicator()));

    /// <summary>
    /// Identifies the <see cref="Thickness"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
        nameof(Thickness),
        typeof(double),
        typeof(KeyboardFocusIndicator),
        new PropertyMetadata(2.0, (o, _) => ((KeyboardFocusIndicator)o).TryRedrawIndicator()), ValidateThickness);

    /// <summary>
    /// Identifies the <see cref="Distance"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DistanceProperty = DependencyProperty.Register(
        nameof(Distance),
        typeof(double),
        typeof(KeyboardFocusIndicator),
        new PropertyMetadata(3.0, (o, _) => ((KeyboardFocusIndicator)o).TryRedrawIndicator()), ValidateOffset);

    /// <summary>
    /// Identifies the <see cref="Target"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
        nameof(Target),
        typeof(FocusTarget),
        typeof(KeyboardFocusIndicator),
        new PropertyMetadata(FocusTarget.Child, OnTargetChanged));

    /// <summary>
    /// Identifies the <see cref="ShapeFamily"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeFamilyProperty = DependencyProperty.Register(
        nameof(ShapeFamily),
        typeof(ShapeFamily),
        typeof(KeyboardFocusIndicator),
        new PropertyMetadata(ShapeFamily.Rounded, (o, _) => ((KeyboardFocusIndicator)o).TryRedrawIndicator()));

    /// <summary>
    /// Identifies the <see cref="ShapeStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeStyleProperty = DependencyProperty.Register(
        nameof(ShapeStyle),
        typeof(ShapeStyle),
        typeof(KeyboardFocusIndicator),
        new PropertyMetadata(ShapeStyle.None, (o, _) => ((KeyboardFocusIndicator)o).TryRedrawIndicator()));

    /// <summary>
    /// Identifies the <see cref="ShapeRadius"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeRadiusProperty = DependencyProperty.Register(
        nameof(ShapeRadius),
        typeof(CornerRadius?),
        typeof(KeyboardFocusIndicator),
        new PropertyMetadata(null, (o, _) => ((KeyboardFocusIndicator)o).TryRedrawIndicator()));

    /// <summary>
    /// Identifies the <see cref="ShapeCorner"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeCornerProperty = DependencyProperty.Register(
        nameof(ShapeCorner),
        typeof(ShapeCorner),
        typeof(KeyboardFocusIndicator),
        new PropertyMetadata(ShapeCorner.All, (o, _) => ((KeyboardFocusIndicator)o).TryRedrawIndicator()));

    /// <summary>
    /// Identifies the <see cref="UseStyleOnRadiusOverride"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UseStyleOnRadiusOverrideProperty = DependencyProperty.Register(
        nameof(UseStyleOnRadiusOverride),
        typeof(bool),
        typeof(KeyboardFocusIndicator),
        new PropertyMetadata(true, (o, _) => ((KeyboardFocusIndicator)o).TryRedrawIndicator()));

    /// <summary>
    /// Identifies the <see cref="UseCornersOnRadiusOverride"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UseCornersOnRadiusOverrideProperty = DependencyProperty.Register(
        nameof(UseCornersOnRadiusOverride),
        typeof(ShapeCorner),
        typeof(KeyboardFocusIndicator),
        new PropertyMetadata(ShapeCorner.All, (o, _) => ((KeyboardFocusIndicator)o).TryRedrawIndicator()));

    /// <summary>
    /// Identifies the <see cref="BindToElementAppearance"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BindToElementAppearanceProperty = DependencyProperty.Register(
        nameof(BindToElementAppearance),
        typeof(bool),
        typeof(KeyboardFocusIndicator),
        new PropertyMetadata(false));

    /// <summary>
    /// Identifies the IndicatorBrush attached property.
    /// </summary>
    public static readonly DependencyProperty IndicatorBrushProperty = DependencyProperty.RegisterAttached(
        "IndicatorBrush",
        typeof(Brush),
        typeof(KeyboardFocusIndicator),
        new PropertyMetadata(Brushes.Black));

    private readonly VisualCollection visuals;
    private readonly ContentPresenter presenter = new();

    private FrameworkElement? parent;

    private bool hasKeyboardFocus;
    private bool isIndicatorRendered;

    static KeyboardFocusIndicator()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(KeyboardFocusIndicator),
            new FrameworkPropertyMetadata(typeof(KeyboardFocusIndicator)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardFocusIndicator"/> class.
    /// </summary>
    public KeyboardFocusIndicator()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;

        visuals = new VisualCollection(this) { presenter };
    }

    /// <summary>
    /// Gets or sets the child element. The default value is <see langword="null"/>.
    /// </summary>
    [Bindable(false)]
    [Category(UICategory.Common)]
    public FrameworkElement? Child
    {
        get => (FrameworkElement?)presenter.Content;
        set
        {
            if (Target is FocusTarget.Child)
            {
                TryRedrawIndicator();

                UnsubscribeFromTargetElementEvents(presenter.Content as FrameworkElement);
                SubscribeToTargetElementEvents(value);
            }

            presenter.Content = value;
        }
    }

    /// <summary>
    /// Gets or sets the <see cref="Brush"/> that is used to draw the indicator. The default value is
    /// <see cref="Brushes.Black"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Brush)]
    public Brush? Brush
    {
        get => (Brush?)GetValue(BrushProperty);
        set => SetValue(BrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the thickness of the indicator. The default value is 2.0.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public double Thickness
    {
        get => (double)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    /// <summary>
    /// Gets or sets the distance between the indicator and the focused element. 0.0 indicates that the indicator should
    /// be drawn directly around the focused element. The default value is 3.0.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public double Distance
    {
        get => (double)GetValue(DistanceProperty);
        set => SetValue(DistanceProperty, value);
    }

    /// <summary>
    /// Gets or sets the target element from which the focus indicator should listen for focus events. The default
    /// value is <see cref="FocusTarget.Parent"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public FocusTarget Target
    {
        get => (FocusTarget)GetValue(TargetProperty);
        set => SetValue(TargetProperty, value);
    }

    /// <summary>
    /// Gets or sets the shape family of the indicator. The default value is <see cref="ShapeFamily.Rounded"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ShapeFamily ShapeFamily
    {
        get => (ShapeFamily)GetValue(ShapeFamilyProperty);
        set => SetValue(ShapeFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the shape style of the indicator. The default value is <see cref="ShapeStyle.None"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ShapeStyle ShapeStyle
    {
        get => (ShapeStyle)GetValue(ShapeStyleProperty);
        set => SetValue(ShapeStyleProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom radius for the indicator. The default value is <see langword="null"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public CornerRadius? ShapeRadius
    {
        get => (CornerRadius?)GetValue(ShapeRadiusProperty);
        set => SetValue(ShapeRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the corners of the indicator that should be rounded. The default value is
    /// <see cref="ShapeCorner.All"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ShapeCorner ShapeCorner
    {
        get => (ShapeCorner)GetValue(ShapeCornerProperty);
        set => SetValue(ShapeCornerProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the indicator should use the shape style when the radius is
    /// overridden. The default value is <see langword="true"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool UseStyleOnRadiusOverride
    {
        get => (bool)GetValue(UseStyleOnRadiusOverrideProperty);
        set => SetValue(UseStyleOnRadiusOverrideProperty, value);
    }

    /// <summary>
    /// Gets or sets the corners of the indicator that should be rounded when the radius is overridden. The default
    /// value is <see cref="ShapeCorner.All"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public ShapeCorner UseCornersOnRadiusOverride
    {
        get => (ShapeCorner)GetValue(UseCornersOnRadiusOverrideProperty);
        set => SetValue(UseCornersOnRadiusOverrideProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the <see cref="KeyboardFocusIndicator"/> should bind appearance properties
    /// to the child or parent element. The default value is <see langword="true"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool BindToElementAppearance
    {
        get => (bool)GetValue(BindToElementAppearanceProperty);
        set => SetValue(BindToElementAppearanceProperty, value);
    }
    
    protected override int VisualChildrenCount => visuals.Count;

    private FrameworkElement? TargetElement => Target is FocusTarget.Parent ? parent : Child;

    /// <summary>
    /// Sets the value of the <see cref="IndicatorBrushProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to set the value of the <see cref="IndicatorBrushProperty"/> property.
    /// </param>
    /// <param name="value">The new value to set the property to.</param>
    public static void SetIndicatorBrush(DependencyObject element, Brush value) =>
        element.SetValue(IndicatorBrushProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IndicatorBrushProperty"/> attached property for a specified dependency object.
    /// </summary>
    /// <param name="element">
    /// The dependency object for which to retrieve the value of the <see cref="IndicatorBrushProperty"/> property.
    /// </param>
    /// <returns>
    /// The current value of the <see cref="IndicatorBrushProperty"/> attached property on the specified dependency
    /// object.
    /// </returns>
    public static Brush GetIndicatorBrush(DependencyObject element) => (Brush)element.GetValue(IndicatorBrushProperty);

    protected override Visual GetVisualChild(int index) => visuals[index];

    protected override Size MeasureOverride(Size availableSize)
    {
        presenter.Measure(availableSize);
        return presenter.DesiredSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        presenter.Arrange(new Rect(finalSize));
        return finalSize;
    }

    protected override void OnVisualParentChanged(DependencyObject? oldParent)
    {
        parent = (TemplatedParent ?? Parent) as FrameworkElement;

        if (Target is FocusTarget.Parent)
        {
            TryRedrawIndicator();

            UnsubscribeFromTargetElementEvents(oldParent as FrameworkElement);
            SubscribeToTargetElementEvents(parent);
        }

        base.OnVisualParentChanged(oldParent);
    }

    private static void OnTargetChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var indicator = (KeyboardFocusIndicator)element;
        if (e.NewValue is FocusTarget.Parent)
        {
            indicator.UnsubscribeFromTargetElementEvents(indicator.Child);
            indicator.SubscribeToTargetElementEvents(indicator.TargetElement);
        }
        else
        {
            indicator.UnsubscribeFromTargetElementEvents(indicator.TargetElement);
            indicator.SubscribeToTargetElementEvents(indicator.Child);
        }
        
        // Rebind the shape properties to the new target.
        if (indicator.BindToElementAppearance)
        {
            indicator.BindShapeProperties(false);
            indicator.BindShapeProperties(true);
        }
    }
    
    private static bool ValidateThickness(object value)
    {
        var thickness = (double)value;
        return thickness >= 0.0 && !double.IsInfinity(thickness) && !double.IsNaN(thickness);
    }

    private static bool ValidateOffset(object value)
    {
        var offset = (double)value;
        return offset >= 0.0 && !double.IsInfinity(offset) && !double.IsNaN(offset);
    }

    private static bool IsKeyboardMostRecentInputDevice()
    {
        // Everything would be easier if the IsKeyboardFocused property was restricted to the keyboard device, but
        // that's not the case. So, we have to do this. Checking the most recent input device is a good enough to
        // replicate the behavior of the FocusVisualStyle property.
        return InputManager.Current.MostRecentInputDevice is KeyboardDevice;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        SetBinding(BrushProperty, new Binding
        {
            Path = new PropertyPath(IndicatorBrushProperty),
            RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
        });

        BindShapeProperties(BindToElementAppearance);
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= OnUnloaded;

        UnsubscribeFromTargetElementEvents(parent);
        UnsubscribeFromTargetElementEvents(Child);
    }

    private void BindShapeProperties(bool bind)
    {
        if (!bind)
        {
            BindingOperations.ClearBinding(this, ShapeFamilyProperty);
            BindingOperations.ClearBinding(this, ShapeStyleProperty);
            BindingOperations.ClearBinding(this, ShapeRadiusProperty);
            BindingOperations.ClearBinding(this, ShapeCornerProperty);
            BindingOperations.ClearBinding(this, UseStyleOnRadiusOverrideProperty);
            BindingOperations.ClearBinding(this, UseCornersOnRadiusOverrideProperty);
            return;
        }
        
        if (Target is FocusTarget.Parent)
        {
            SetBinding(ShapeFamilyProperty, new Binding
            {
                Path = new PropertyPath(Shape.FamilyProperty),
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });

            SetBinding(ShapeStyleProperty, new Binding
            {
                Path = new PropertyPath(Shape.StyleProperty),
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });

            SetBinding(ShapeRadiusProperty, new Binding
            {
                Path = new PropertyPath(Shape.RadiusProperty),
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });

            SetBinding(ShapeCornerProperty, new Binding
            {
                Path = new PropertyPath(Shape.CornerProperty),
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });

            SetBinding(UseStyleOnRadiusOverrideProperty, new Binding
            {
                Path = new PropertyPath(Shape.UseStyleOnRadiusOverrideProperty),
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });

            SetBinding(UseCornersOnRadiusOverrideProperty, new Binding
            {
                Path = new PropertyPath(Shape.UseCornersOnRadiusOverrideProperty),
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            });
        }
        else
        {
            SetBinding(ShapeFamilyProperty, new Binding
            {
                Path = new PropertyPath(Shape.FamilyProperty),
                Source = Child
            });

            SetBinding(ShapeStyleProperty, new Binding
            {
                Path = new PropertyPath(Shape.StyleProperty),
                Source = Child
            });

            SetBinding(ShapeRadiusProperty, new Binding
            {
                Path = new PropertyPath(Shape.RadiusProperty),
                Source = Child
            });

            SetBinding(ShapeCornerProperty, new Binding
            {
                Path = new PropertyPath(Shape.CornerProperty),
                Source = Child
            });

            SetBinding(UseStyleOnRadiusOverrideProperty, new Binding
            {
                Path = new PropertyPath(Shape.UseStyleOnRadiusOverrideProperty),
                Source = Child
            });

            SetBinding(UseCornersOnRadiusOverrideProperty, new Binding
            {
                Path = new PropertyPath(Shape.UseCornersOnRadiusOverrideProperty),
                Source = Child
            });
        }
    }

    private void SubscribeToTargetElementEvents(FrameworkElement? element)
    {
        if (element is null)
        {
            return;
        }

        element.GotFocus += OnTargetGotFocus;
        element.LostFocus += OnTargetLostFocus;
        element.SizeChanged += OnTargetSizeChanged;
    }

    private void UnsubscribeFromTargetElementEvents(FrameworkElement? element)
    {
        if (element is null)
        {
            return;
        }

        element.GotFocus -= OnTargetGotFocus;
        element.LostFocus -= OnTargetLostFocus;
        element.SizeChanged -= OnTargetSizeChanged;
    }

    private void OnTargetGotFocus(object sender, RoutedEventArgs e) =>
        DrawIndicator(IsKeyboardMostRecentInputDevice());

    private void OnTargetLostFocus(object sender, RoutedEventArgs e) => DrawIndicator(false);

    private void OnTargetSizeChanged(object sender, SizeChangedEventArgs e) => TryRedrawIndicator();
    
    private void TryRedrawIndicator()
    {
        if (hasKeyboardFocus && isIndicatorRendered)
        {
            DrawIndicator(true, isRedraw: true);
        }
    }

    private void DrawIndicator(bool hasFocus, bool isRedraw = false)
    {
        hasKeyboardFocus = hasFocus;

        if (!hasFocus)
        {
            if (isIndicatorRendered)
            {
                visuals.RemoveAt(1);
                isIndicatorRendered = false;
            }

            return;
        }

        var brush = Brush;
        var thickness = Thickness;

        if (TargetElement is null ||
            TargetElement.RenderSize is { Width: 0.0 } or { Height: 0.0 } ||
            brush is null ||
            thickness is 0.0)
        {
            return;
        }

        var visual = isRedraw ? (DrawingVisual)visuals[1] : new DrawingVisual();
        using var context = visual.RenderOpen();

        var distance = Distance;
        var bounds = new Rect(RenderSize);

        bounds.Inflate(distance, distance);

        var radius = ShapeRadius is null
            ? ShapeHelper.GetRadiusForStyle(ShapeStyle, ShapeCorner, bounds.Width, bounds.Height)
            : ShapeHelper.ClampCornerRadius(
                ShapeRadius.Value,
                ShapeStyle,
                bounds.Width,
                bounds.Height,
                UseStyleOnRadiusOverride,
                UseCornersOnRadiusOverride);

        var geometry = ShapeGeometry.Create(
            ShapeFamily,
            bounds,
            radius,
            isFilled: false,
            isStroked: true);

        context.DrawGeometry(brush, new Pen(brush, thickness), geometry);

        if (isRedraw)
        {
            return;
        }

        visuals.Insert(1, visual);
        isIndicatorRendered = true;
    }
}