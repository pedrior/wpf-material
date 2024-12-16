using System.Windows.Input;

namespace WPF.Material.Components;

/// <summary>
/// Represents a control that a user can select and clear.
/// </summary>
[TemplatePart(Name = PartPanel, Type = typeof(Panel))]
[TemplatePart(Name = PartRipple, Type = typeof(Ripple))]
public class CheckBox : System.Windows.Controls.CheckBox
{
    /// <summary>
    /// Identifies the <see cref="IsContentClickable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsContentClickableProperty = DependencyProperty.Register(
        nameof(IsContentClickable),
        typeof(bool),
        typeof(CheckBox),
        new PropertyMetadata(true));

    private const string PartPanel = "PART_Panel";
    private const string PartRipple = "PART_Ripple";

    private Panel? panel;
    private Ripple? ripple;

    static CheckBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(CheckBox),
            new FrameworkPropertyMetadata(typeof(CheckBox)));
    }

    /// <summary>
    /// Gets or sets a value indicating whether the content element should also change the checkbox state when clicked.
    /// When <see langword="false"/>, the state will only change when the checkbox itself (i.e. the icon) is clicked.
    /// The default value is <see langword="true"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsContentClickable
    {
        get => (bool)GetValue(IsContentClickableProperty);
        set => SetValue(IsContentClickableProperty, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (panel is not null)
        {
            panel.MouseLeftButtonDown -= OnPanelMouseLeftButtonDown;
            panel.MouseLeftButtonUp -= OnPanelMouseLeftButtonUp;
            panel.MouseLeave -= OnPanelMouseLeave;
        }

        panel = GetTemplateChild(PartPanel) as Panel ??
                throw new NullReferenceException($"Missing required template part '{PartPanel}'.");

        ripple = GetTemplateChild(PartRipple) as Ripple ??
                 throw new NullReferenceException($"Missing required template part '{PartRipple}'.");

        panel.MouseLeftButtonDown += OnPanelMouseLeftButtonDown;
        panel.MouseLeftButtonUp += OnPanelMouseLeftButtonUp;
        panel.MouseLeave += OnPanelMouseLeave;
    }

    private void OnPanelMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => 
        ripple!.Start(e.GetPosition(ripple));

    private void OnPanelMouseLeftButtonUp(object sender, MouseButtonEventArgs e) => ripple!.Release();

    private void OnPanelMouseLeave(object sender, MouseEventArgs e) => ripple!.Release();
}