using DailyFitNative.Infrastructure.DependencyInjection;
using DailyFitNative.Utilities.Navigation.Abstraction;

namespace DailyFitNative.Utilities.Navigation
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
