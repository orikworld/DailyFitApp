using CRSTNative.Client.Infrastructure.Utilities.Navigation.Abstraction;
using CRSTNative.Infrastructure.DependencyInjection;

namespace CRSTNative.Client.Infrastructure.Utilities.Navigation
{
    /// <summary>
    /// NavigationService
    /// </summary>
    public class NavigationService
    {

        #region Construtors

        /// <summary>
        /// Prevents a default instance of the <see cref="NavigationService"/> class from being created.
        /// </summary>
        private NavigationService() { }

        /// <summary>
        /// Initializes the <see cref="NavigationService"/> class.
        /// </summary>
        static NavigationService()
        {
            Instance = DependencyManager.Instance.ServiceLocator.GetInstance<INavigationService>();
        }

        #endregion

        #region Instance

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static INavigationService Instance { get; private set; }

        #endregion
    }
}
