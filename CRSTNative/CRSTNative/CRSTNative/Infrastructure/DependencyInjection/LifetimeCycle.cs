namespace DailyFitNative.Infrastructure.DependencyInjection
{
    public enum LifetimeCycle
    {
        NewInstance = 0,
        SingletonInstance = 1,
        PerThreadInstance = 2,
    }
}
