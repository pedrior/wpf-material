using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace WPF.Material.Components;

[ContentProperty(nameof(Content))]
internal sealed class SwitchThumb : Thumb
{
    public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
        nameof(Content),
        typeof(object),
        typeof(SwitchThumb),
        new PropertyMetadata(null));

    static SwitchThumb()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SwitchThumb),
            new FrameworkPropertyMetadata(typeof(SwitchThumb)));
    }

    public object? Content
    {
        get => (object?)GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }
}