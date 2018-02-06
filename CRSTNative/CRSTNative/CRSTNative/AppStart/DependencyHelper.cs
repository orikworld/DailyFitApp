﻿using CRSTNative.Client.Infrastructure.Core.ViewModels.Abstractions;
using CRSTNative.Client.Infrastructure.Utilities.Navigation;
using CRSTNative.Client.Infrastructure.Utilities.Navigation.Abstraction;
using CRSTNative.Client.Infrastructure.Utilities.Navigation.Implementation;
using CRSTNative.Infrastructure.DependencyInjection;
using CRSTNative.Infrastructure.DependencyInjection.Interfaces;
using CRSTNative.Modules.Dashboard;
using CRSTNative.Modules.Login;
using CRSTNative.Modules.Menu;
using Xamarin.Forms;

namespace CRSTNative.AppStart
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
            container.RegisterDependency<Page, LoginPage, BaseViewModel>(
                ViewId.LoginPage.ToString());

            container.RegisterDependency<BaseViewModel, LoginViewModel>(ViewId.LoginPage.ToString());
        }

        #endregion        
    }
}
