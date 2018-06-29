using DailyFitNative.Infrastructure.DependencyInjection;
using DailyFitNative.Infrastructure.Utilities.Navigation.Abstraction;

namespace DailyFitNative.Infrastructure.Utilities.Navigation
{
	public class NavigationService
    {
        #region Construtors

        private NavigationService() { }

        static NavigationService()
        {
            Instance = DependencyManager.Instance.ServiceLocator.GetInstance<INavigationService>();
        }

        #endregion

        #region Instance

        public static INavigationService Instance { get; private set; }

        #endregion
    }
}
