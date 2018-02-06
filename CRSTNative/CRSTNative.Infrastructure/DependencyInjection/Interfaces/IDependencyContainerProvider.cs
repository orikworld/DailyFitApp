using CommonServiceLocator;

namespace CRSTNative.Infrastructure.DependencyInjection.Interfaces
{
    /// <summary>
    /// IDependencyContainerProvider
    /// </summary>
    /// <seealso cref="CRSTNative.Infrastructure.DependencyInjection.Interfaces.ICustomContainer" />
    /// <seealso cref="CommonServiceLocator.IServiceLocator" />
    internal interface IDependencyContainerProvider: ICustomContainer, IServiceLocator
    {
    }
}
