using DailyFitNative.Infrastructure.Core.ViewModels.Abstractions;
using DailyFitNative.Infrastructure.DependencyInjection;
using DailyFitNative.Infrastructure.DependencyInjection.Interfaces;
using DailyFitNative.Infrastructure.Utilities.Navigation;
using DailyFitNative.Infrastructure.Utilities.Navigation.Abstraction;
using DailyFitNative.Infrastructure.Utilities.Navigation.Implementation;
using DailyFitNative.Modules.Authorization.Login;
using DailyFitNative.Modules.Authorization.Registration;
using DailyFitNative.Modules.Dashboard;
using DailyFitNative.Modules.Login;
using DailyFitNative.Modules.Menu;
using Xamarin.Forms;

namespace DailyFitNative.AppStart
{
    public class DependencyHelper
    {
        #region Public Methods

        public static void SetDependencies()
        {
            var container = DependencyManager.Instance.Container;

            RegisteLoginDependencies(container);
            RegisteMenuDependencies(container);
            RegisterDashboardDependencies(container);
        }

        public static void SetNavigationInstance(INavigation formsNavigation)
        {
            var container = DependencyManager.Instance.Container;

            container.RegisterInstance<INavigationService>(new NavigationServiceImplementation(formsNavigation), LifetimeCycle.SingletonInstance);
        }

        #endregion

        #region Private Methods

        private static void RegisterDashboardDependencies(ICustomContainer container)
        {
            container.RegisterDependency<Page, DashboardPage, BaseViewModel>(
                ViewId.DashboardPage.ToString());

            container.RegisterDependency<BaseViewModel, DashboardViewModel>(ViewId.DashboardPage.ToString());
        }

        private static void RegisteMenuDependencies(ICustomContainer container)
        {
            container.RegisterDependency<Page, MenuPage, BaseViewModel>(
                ViewId.MenuPage.ToString());

            container.RegisterDependency<BaseViewModel, MenuViewModel>(ViewId.MenuPage.ToString());
        }

        private static void RegisteLoginDependencies(ICustomContainer container)
        {
            container.RegisterDependency<Page, LoginPage, BaseViewModel>(ViewId.LoginPage.ToString());
            container.RegisterDependency<BaseViewModel, LoginViewModel>(ViewId.LoginPage.ToString());

            container.RegisterDependency<Page, RegistrationPage, BaseViewModel>(ViewId.RegistrationPage.ToString());
            container.RegisterDependency<BaseViewModel, RegistrationViewModel>(ViewId.RegistrationPage.ToString());
        }

        #endregion        
    }
}
