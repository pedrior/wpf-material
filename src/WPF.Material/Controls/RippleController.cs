namespace WPF.Material.Controls;

/// <summary>
/// Represents a controller that can be used to control the ripple effect of a <see cref="Ripple"/>.
/// </summary>
public sealed class RippleController
{
    private readonly Ripple ripple;

    internal RippleController(Ripple ripple)
    {
        this.ripple = ripple;
    }
    
    /// <summary>
    /// Starts the ripple effect and releases it automatically.
    /// </summary>
    public void StartAndRelease() => ripple.Start();
    
    /// <summary>
    /// Starts the ripple effect and holds it until <see cref="Release"/> is called.
    /// </summary>
    public void StartAndKeep() => ripple.Start(true);
    
    /// <summary>
    /// Release the ripple effect wherever it is.
    /// </summary>
    public void Release() => ripple.Release();
}