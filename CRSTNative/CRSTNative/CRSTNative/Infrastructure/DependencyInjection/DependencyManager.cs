using CommonServiceLocator;
using DailyFitNative.Infrastructure.DependencyInjection.Implementation;
using DailyFitNative.Infrastructure.DependencyInjection.Interfaces;

namespace DailyFitNative.Infrastructure.DependencyInjection
{
    public class DependencyManager
    {
        #region Private Fields

        private readonly IDependencyContainerProvider _singletonDependencyContainerProvider;

        #endregion

        #region Constructors

        private DependencyManager()
        {
            _singletonDependencyContainerProvider = new UnityImplementation();
        }

        #endregion

        #region Properties

        public static DependencyManager Instance { get; } = new DependencyManager();

        public ICustomContainer Container => _singletonDependencyContainerProvider;

        public IServiceLocator ServiceLocator => _singletonDependencyContainerProvider;

        #endregion
    }
}
