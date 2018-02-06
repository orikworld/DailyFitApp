namespace CRSTNative.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Declares Lifetime cycle options
    /// </summary>
    public enum LifetimeCycle
    {
        NewInstance = 0,
        SingletonInstance = 1,
        PerThreadInstance = 2,
    }
}
