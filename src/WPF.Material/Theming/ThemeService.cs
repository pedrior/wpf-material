namespace WPF.Material.Theming;

/// <summary>
/// A service that manages the application's theme.
/// </summary>
public sealed class ThemeService
{
    private static readonly Lazy<ThemeService> LazyInstance = new(
        () => new ThemeService(),
        isThreadSafe: true);

    private ThemeResources? themeResourceDictionary;

    /// <summary>
    /// Occurs when the <see cref="ThemeService"/> has been initialized.
    /// </summary>
    public event EventHandler? Initialized;

    /// <summary>
    /// Gets the singleton instance of the <see cref="ThemeService"/>.
    /// </summary>
    public static ThemeService Instance => LazyInstance.Value;
    
    /// <summary>
    /// Gets a value indicating whether the <see cref="ThemeService"/> has been initialized.
    /// </summary>
    public bool IsInitialized { get; private set; }

    /// <summary>
    /// Gets or sets the application's current theme.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the <see cref="ThemeService"/> has not been initialized yet.
    /// </exception>
    public Theme Theme
    {
        get
        {
            ThrowExceptionIfNotInitialized();
            return themeResourceDictionary!.Theme;
        }
        set
        {
            ThrowExceptionIfNotInitialized();
            themeResourceDictionary!.Theme = value;
        }
    }

    internal void Initialize(ThemeResources resources)
    {
        themeResourceDictionary = resources;
        IsInitialized = true;
        
        OnInitialized();
    }
    
    private void ThrowExceptionIfNotInitialized()
    {
        if (!IsInitialized)
        {
            throw new InvalidOperationException("The Theme Manager has not been initialized yet.");
        }
    }

    private void OnInitialized() => Initialized?.Invoke(this, EventArgs.Empty);
}