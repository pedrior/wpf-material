﻿using System.Windows.Input;

namespace WPF.Material.Components;

/// <summary>
/// Represents an item in a <see cref="NavigationRail"/> component.
/// </summary>
[TemplatePart(Name = PartRipple, Type = typeof(Container))]
public class NavigationItem : System.Windows.Controls.RadioButton
{
    private const string PartRipple = "PART_Ripple";

    private Ripple? ripple;

    static NavigationItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationItem),
            new FrameworkPropertyMetadata(typeof(NavigationItem)));
    }
    
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ripple = GetTemplateChild(PartRipple) as Ripple;
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        ripple?.Start(e.GetPosition(ripple));
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonUp(e);

        ripple?.Release();
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        base.OnMouseLeave(e);

        ripple?.Release();
    }

    protected override void OnChecked(RoutedEventArgs e)
    {
        base.OnChecked(e);

        if (Parent is Navigation navigation)
        {
            navigation.SetItemIsSelected(this);
        }
    }
}