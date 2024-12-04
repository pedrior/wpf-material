using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace WPF.Material.Styles;

internal sealed class ThemeRunner
{
    private const string TypefaceName = "Typefaces.Default";

    private readonly ResourceDictionary resources;

    private Theme? appliedTheme;

    public ThemeRunner(ResourceDictionary resources)
    {
        this.resources = resources;
    }

    public void ApplyTheme(Theme theme)
    {
        ApplyColorScheme(theme.Colors);
        ApplyTypography(theme.Typeface);

        appliedTheme = theme;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming",
        Justification = "Naming is consistent with the format 'Material.Colors.*'")]
    private void ApplyColorScheme(ColorScheme Colors)
    {
        UpdateColorResource(Colors.Primary, appliedTheme?.Colors.Primary);
        UpdateColorResource(Colors.OnPrimary, appliedTheme?.Colors.OnPrimary);
        UpdateColorResource(Colors.PrimaryContainer, appliedTheme?.Colors.PrimaryContainer);
        UpdateColorResource(Colors.OnPrimaryContainer, appliedTheme?.Colors.OnPrimaryContainer);
        UpdateColorResource(Colors.PrimaryFixed, appliedTheme?.Colors.PrimaryFixed);
        UpdateColorResource(Colors.OnPrimaryFixed, appliedTheme?.Colors.OnPrimaryFixed);
        UpdateColorResource(Colors.PrimaryFixedDim, appliedTheme?.Colors.PrimaryFixedDim);
        UpdateColorResource(Colors.OnPrimaryFixedVariant, appliedTheme?.Colors.OnPrimaryFixedVariant);
        UpdateColorResource(Colors.Secondary, appliedTheme?.Colors.Secondary);
        UpdateColorResource(Colors.OnSecondary, appliedTheme?.Colors.OnSecondary);
        UpdateColorResource(Colors.SecondaryContainer, appliedTheme?.Colors.SecondaryContainer);
        UpdateColorResource(Colors.OnSecondaryContainer, appliedTheme?.Colors.OnSecondaryContainer);
        UpdateColorResource(Colors.SecondaryFixed, appliedTheme?.Colors.SecondaryFixed);
        UpdateColorResource(Colors.OnSecondaryFixed, appliedTheme?.Colors.OnSecondaryFixed);
        UpdateColorResource(Colors.SecondaryFixedDim, appliedTheme?.Colors.SecondaryFixedDim);
        UpdateColorResource(Colors.OnSecondaryFixedVariant, appliedTheme?.Colors.OnSecondaryFixedVariant);
        UpdateColorResource(Colors.Tertiary, appliedTheme?.Colors.Tertiary);
        UpdateColorResource(Colors.OnTertiary, appliedTheme?.Colors.OnTertiary);
        UpdateColorResource(Colors.TertiaryContainer, appliedTheme?.Colors.TertiaryContainer);
        UpdateColorResource(Colors.OnTertiaryContainer, appliedTheme?.Colors.OnTertiaryContainer);
        UpdateColorResource(Colors.TertiaryFixed, appliedTheme?.Colors.TertiaryFixed);
        UpdateColorResource(Colors.OnTertiaryFixed, appliedTheme?.Colors.OnTertiaryFixed);
        UpdateColorResource(Colors.TertiaryFixedDim, appliedTheme?.Colors.TertiaryFixedDim);
        UpdateColorResource(Colors.OnTertiaryFixedVariant, appliedTheme?.Colors.OnTertiaryFixedVariant);
        UpdateColorResource(Colors.Error, appliedTheme?.Colors.Error);
        UpdateColorResource(Colors.OnError, appliedTheme?.Colors.OnError);
        UpdateColorResource(Colors.ErrorContainer, appliedTheme?.Colors.ErrorContainer);
        UpdateColorResource(Colors.OnErrorContainer, appliedTheme?.Colors.OnErrorContainer);
        UpdateColorResource(Colors.Surface, appliedTheme?.Colors.Surface);
        UpdateColorResource(Colors.OnSurface, appliedTheme?.Colors.OnSurface);
        UpdateColorResource(Colors.OnSurfaceVariant, appliedTheme?.Colors.OnSurfaceVariant);
        UpdateColorResource(Colors.SurfaceDim, appliedTheme?.Colors.SurfaceDim);
        UpdateColorResource(Colors.SurfaceBright, appliedTheme?.Colors.SurfaceBright);
        UpdateColorResource(Colors.SurfaceContainerHighest, appliedTheme?.Colors.SurfaceContainerHighest);
        UpdateColorResource(Colors.SurfaceContainerHigh, appliedTheme?.Colors.SurfaceContainerHigh);
        UpdateColorResource(Colors.SurfaceContainer, appliedTheme?.Colors.SurfaceContainer);
        UpdateColorResource(Colors.SurfaceContainerLow, appliedTheme?.Colors.SurfaceContainerLow);
        UpdateColorResource(Colors.SurfaceContainerLowest, appliedTheme?.Colors.SurfaceContainerLowest);
        UpdateColorResource(Colors.InverseSurface, appliedTheme?.Colors.InverseSurface);
        UpdateColorResource(Colors.InverseOnSurface, appliedTheme?.Colors.InverseOnSurface);
        UpdateColorResource(Colors.InversePrimary, appliedTheme?.Colors.InversePrimary);
        UpdateColorResource(Colors.Outline, appliedTheme?.Colors.Outline);
        UpdateColorResource(Colors.OutlineVariant, appliedTheme?.Colors.OutlineVariant);
        UpdateColorResource(Colors.Scrim, appliedTheme?.Colors.Scrim);
    }

    private void ApplyTypography(string typeface)
    {
        if (typeface != appliedTheme?.Typeface)
        {
            resources[MakeResourceKey(TypefaceName)] = Typefaces.GetTypefaceOrDefault(typeface);
        }
    }

    private void UpdateColorResource(Color @new, Color? old, [CallerArgumentExpression("new")] string name = "")
    {
        if (@new != old)
        {
            resources[MakeResourceKey(name)] = @new.ToBrush();
        }
    }

    private static object MakeResourceKey(string name) => $"Material.{name}";
}