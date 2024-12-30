# WPF Material

An implementation of [Google's Material Design 3](https://m3.material.io/) for WPF and .NET >= 6.

> **Note:** This project is in its early stages. I will provide more information and documentation as it progresses.

![WPF Material](./images/gallery.png)

## 🖼️ Here's a demo of how the project is going so far

### Common Button

![Common Button](./images/button.gif)

```xaml
<md3:Button Content="{content}" Type="{Filled | Tonal | Elevated | Outlined | Text}" />
```

#### Common button with icons

![Common Button](./images/button-icon.gif)

```xaml
<md3:Button 
    md3:Icon.RestSymbol="{icon}"
    Content="{content}" 
    Type="{Filled | Tonal | Elevated | Outlined | Text}" />
```

#### Common button with advanced icon settings

![Common Button](./images/button-icon-advanced.gif)

```xaml
<md3:Button 
    md3:Icon.RestSymbol="{icon}"
    md3:Icon.HoverSymbol="{icon}"
    md3:Icon.PressSymbol="{icon}"
    Content="{content}" 
    Type="{Filled | Tonal | Elevated | Outlined | Text}" />
```

### Floating Action Button (FAB)

FAB's with different sizes (small, standard and large) and types (surface, primary, secondary and tertiary).

![Common Button](./images/fab.gif)

```xaml
<md3:FloatingActionButton 
    Size="{Small | Standard | Large}" 
    Type="{Surface | Primary | Secondary | Tertiary}" />
```

#### Extended FAB

![Common Button](./images/fab-extended.gif)

```xaml
<md3:FloatingActionButton 
    Content="{content}"
    CanExtend="True"
    IsExtended="{True | False}"
    Type="{Surface | Primary | Secondary | Tertiary}" />
```

### Segmented Buttons

![Common Button](./images/segmented-buttons.gif)

```xaml
<md3:SegmentedButtonGroup>
    <md3:SegmentedButton
        md3:Icon.RestSymbol="{icon}"
        md3:Icon.SelectionSymbol="{icon | null (remove checkmark)}"
        Content="{content}" />
        
    ...
</md3:SegmentedButtonGroup>
```

Segmented buttons with multiple selection

![Common Button](./images/segmented-buttons-multi-select.gif)

```xaml
<md3:SegmentedButtonGroup IsMultiSelect="{True | False}">
    <md3:SegmentedButton
        md3:Icon.RestSymbol="{icon}"
        md3:Icon.SelectionSymbol="{icon | null (remove checkmark)}"
        Content="{content}" />
        
    ...
</md3:SegmentedButtonGroup>
```

Segmented buttons with selection required

![Common Button](./images/segmented-buttons-selection-required.gif)

```xaml
<md3:SegmentedButtonGroup IsMultiSelect="{True | False}">
    <md3:SegmentedButton
        md3:Icon.RestSymbol="{icon}"
        md3:Icon.SelectionSymbol="{icon | null (remove checkmark)}"
        Content="{content}" />
        
    ...
</md3:SegmentedButtonGroup>
```

### Check Box

![Common Button](./images/checkbox.gif)

```xaml
<md3:CheckBox Content="{content}" IsContentClickable="{True | False}"/>
```

### Radio Button

![Common Button](./images/radiobutton.gif)

```xaml
<md3:RadioButton Content="{content}" IsContentClickable="{True | False}"/>
```

### Card

![Common Button](./images/card.gif)

```xaml
<md3:Card Content="{content}" IsClickable="{True | False}"/>
```

#### Card types

![Common Button](./images/card-types.gif)

```xaml
<md3:Card Content="{content}" Type="{Outlined | Filled | Elevated}"/>
```

## 📜 License

This project is licensed under the [MIT License](LICENSE).
