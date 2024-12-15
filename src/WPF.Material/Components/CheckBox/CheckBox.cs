using System.Windows.Input;

namespace WPF.Material.Components;

/// <summary>
/// Represents a control that a user can select and clear.
/// </summary>
[TemplatePart(Name = PartPanel, Type = typeof(Panel))]
[TemplatePart(Name = PartRipple, Type = typeof(Ripple))]
[TemplatePart(Name = PartContainer, Type = typeof(Container))]
public class CheckBox : System.Windows.Controls.CheckBox
{
    /// <summary>
    /// Identifies the <see cref="IsPanelHitTestVisible"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsPanelHitTestVisibleProperty = DependencyProperty.Register(
        nameof(IsPanelHitTestVisible),
        typeof(bool),
        typeof(CheckBox),
        new PropertyMetadata(true));

    private const string PartPanel = "PART_Panel";
    private const string PartRipple = "PART_Ripple";
    private const string PartContainer = "PART_Container";

    private Panel? panel;
    private Ripple? ripple;

    static CheckBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(CheckBox),
            new FrameworkPropertyMetadata(typeof(CheckBox)));
    }

    /// <summary>
    /// Gets or sets a value indicating whether the root panel is hit test visible. When set to <see langword="true"/>,
    /// the state of the checkbox can be changed by clicking anywhere on the control. When set to <see langword="false"/>,
    /// the state of the checkbox can only be changed by clicking on the checkbox itself.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Common)]
    public bool IsPanelHitTestVisible
    {
        get => (bool)GetValue(IsPanelHitTestVisibleProperty);
        set => SetValue(IsPanelHitTestVisibleProperty, value);
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