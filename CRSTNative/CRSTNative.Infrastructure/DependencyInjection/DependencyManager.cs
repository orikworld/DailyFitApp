using CommonServiceLocator;
using CRSTNative.Infrastructure.DependencyInjection.Implementation;
using CRSTNative.Infrastructure.DependencyInjection.Interfaces;

namespace CRSTNative.Infrastructure.DependencyInjection
{
    /// <summary>
    /// DependencyManager
    /// </summary>
    public class DependencyManager
    {
        #region Private Fields

        /// <summary>
        /// The singleton dependency container provider
        /// </summary>
        private readonly IDependencyContainerProvider _singletonDependencyContainerProvider;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="DependencyManager" /> class from being created.
        /// </summary>
        private DependencyManager()
        {
            _singletonDependencyContainerProvider = new UnityImplementation();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static DependencyManager Instance { get; } = new DependencyManager();

        /// <summary>
        /// Gets the container.
        /// </summary>
        public ICustomContainer Container => _singletonDependencyContainerProvider;

        /// <summary>
        /// Gets the service locator.
        /// </summary>
        public IServiceLocator ServiceLocator => _singletonDependencyContainerProvider;

        #endregion

    }
}
