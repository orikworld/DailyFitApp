using CommonServiceLocator;

namespace DailyFitNative.Infrastructure.DependencyInjection.Interfaces
{
    internal interface IDependencyContainerProvider: ICustomContainer, IServiceLocator
    {
    }
}
