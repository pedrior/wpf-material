using WPF.Material.Assists;
using WPF.Material.Environment;

namespace WPF.Material.Controls;

/// <summary>
/// Represents a toggle button that is part of a <see cref="ToggleButtonGroup"/>.
/// </summary>
public class ToggleButton : ToggleButtonBase
{
    /// <summary>
    /// Identifies the <see cref="ShowIcon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowIconProperty = DependencyProperty.Register(
        nameof(ShowIcon),
        typeof(bool),
        typeof(ToggleButton),
        new PropertyMetadata(true));
    
    static ToggleButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ToggleButton),
            new FrameworkPropertyMetadata(typeof(ToggleButton)));
    }
    
    /// <summary>
    /// Gets or sets a value indicating whether the icon should be shown. When the button is checked, the
    /// <see cref="IconAssist.IconOnSelectingProperty"/> will be used, otherwise the <see cref="IconAssist.IconProperty"/>.
    /// </summary>
    [Bindable(true)]
    [Category(UICategory.Appearance)]
    public bool ShowIcon
    {
        get => (bool)GetValue(ShowIconProperty);
        set => SetValue(ShowIconProperty, value);
    }
    
    protected override void OnToggle()
    {
        if (Parent is ToggleButtonGroup group)
        {
            group.ToggleItem(this);
        }
        else
        {
            base.OnToggle();
        }
    }
}