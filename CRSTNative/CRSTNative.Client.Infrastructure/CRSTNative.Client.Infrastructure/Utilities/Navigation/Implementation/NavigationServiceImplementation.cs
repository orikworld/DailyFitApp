using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRSTNative.Client.Infrastructure.Core.Views.Abstractions;
using CRSTNative.Client.Infrastructure.Core.Views.Implementations;
using CRSTNative.Client.Infrastructure.Utilities.Navigation.Abstraction;
using CRSTNative.Infrastructure.DependencyInjection;
using CRSTNative.Infrastructure.Resources;
using Xamarin.Forms;

namespace CRSTNative.Client.Infrastructure.Utilities.Navigation.Implementation
{
    /// <summary>
    /// NavigationServiceImplementation
    /// </summary>
    public class NavigationServiceImplementation : INavigationService
    {
        #region Private Fields

        /// <summary>
        /// The master detail page
        /// </summary>
        private MasterDetailPage _masterDetailPage;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationServiceImplementation"/> class.
        /// </summary>
        /// <param name="navigation">The navigation.</param>
        public NavigationServiceImplementation(INavigation navigation)
        {
            FormsNavigation = navigation;
            ModalStack = navigation.ModalStack;
            NavigationStack = navigation.NavigationStack;
        }

        #endregion

        #region Properties

        public IReadOnlyList<Page> ModalStack { get; private set; }

        public  IReadOnlyList<Page> NavigationStack { get; private set; }

        public INavigation FormsNavigation { get; private set; }

        public bool IsLastPage => NavigationStack.Count > 0;

        public bool IsMainNavigationStack => true;

        #endregion

        #region INavigationServiceImplementation

        public void InsertPageBefore(Page page, Page before)
        {
           FormsNavigation.InsertPageBefore(page, before);
        }

        public Task<Page> PopAsync()
        {
            return FormsNavigation.PopAsync();
        }

        public Task<Page> PopAsync(bool animated)
        {
            return FormsNavigation.PopAsync(animated);
        }

        public Task<Page> PopModalAsync()
        {
            return FormsNavigation.PopModalAsync();
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            return FormsNavigation.PopModalAsync(animated);
        }

        public Task PopToRootAsync()
        {
            return FormsNavigation.PopToRootAsync();
        }

        public Task PopToRootAsync(bool animated)
        {
            return FormsNavigation.PopToRootAsync(animated);
        }

        public Task PushAsync(Page page)
        {
            return FormsNavigation.PushAsync(page);
        }

        public Task PushAsync(Page page, bool animated)
        {
            return FormsNavigation.PushAsync(page, animated);
        }

        public Task PushModalAsync(Page page)
        {
            return FormsNavigation.PushModalAsync(page);
        }

        public Task PushModalAsync(Page page, bool animated)
        {
            return FormsNavigation.PushModalAsync(page, animated);
        }


        /// <summary>
        /// Removes the specified page from the navigation stack.
        /// </summary>
        /// <param name="page">The page to remove.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        // toDo do not use this method before issue "https://bugzilla.xamarin.com/show_bug.cgi?id=53179" is fixed and android package update. Use Pop instead
        public void RemovePage(Page page)
        {
            (page as IBasePage)?.OnPagePopped();
            FormsNavigation.RemovePage(page);
        }

        public Task NavigateTo(ViewId viewId, bool animated = false)
        {
            var page = DependencyManager.Instance.ServiceLocator.GetInstance<Page>(viewId.ToString());

            if (page is IBasePage mainPage)
            {
                mainPage.PageId = viewId;
            }

            return PushAsync(page, animated);
        }

        public Task NavigateToModal(ViewId viewId, bool animated = false)
        {
            var page = DependencyManager.Instance.ServiceLocator.GetInstance<Page>(viewId.ToString());

            if (page is IBasePage mainPage)
            {
                mainPage.PageId = viewId;
            }

            return PushModalAsync(page, animated);
        }

        public Task SetRootPageAsunc(ViewId viewId, bool animated = false)
        {
            if (viewId == ViewId.MenuPage)
            {
                _masterDetailPage = new BaseMasterDetailPage()
                {
                    Detail = new BaseNavigationPage(
                        DependencyManager.Instance.ServiceLocator.GetInstance<Page>(ViewId.DashboardPage.ToString())),
                    MasterBehavior = MasterBehavior.Popover,
                };
               

                SetMenuPage(viewId);

                FormsNavigation = _masterDetailPage.Detail.Navigation;
                ModalStack = FormsNavigation.ModalStack;
                NavigationStack = FormsNavigation.NavigationStack;

                Application.Current.MainPage = _masterDetailPage;
            }
            else
            {
                SetRootPage(DependencyManager.Instance.ServiceLocator.GetInstance<Page>(viewId.ToString()));
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Removes the page.
        /// </summary>
        /// <param name="viewId">The view identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        /// toDo do not use this method before issue "https://bugzilla.xamarin.com/show_bug.cgi?id=53179" is fixed and android package update. Use Pop instead
        public void RemovePage(ViewId viewId)
        {
            var pageToRemove = NavigationStack.FirstOrDefault(i => (i as IBasePage)?.PageId == viewId) ??
                               ModalStack.FirstOrDefault(i => (i as IBasePage)?.PageId == viewId);

            RemovePage(pageToRemove);
        }

        public void SetRootPage(Page menuItemMenuPage)
        {
            var page = new BaseNavigationPage(menuItemMenuPage);

            _masterDetailPage.Detail = page;
            _masterDetailPage.IsPresented = false;

            FormsNavigation = _masterDetailPage.Detail.Navigation;
            ModalStack = FormsNavigation.ModalStack;
            NavigationStack = FormsNavigation.NavigationStack;
        }

        public void NavigateFromMasterPage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Hides the slider menu.
        /// </summary>
        public void HideSliderMenu()
        {
            if (_masterDetailPage != null)
            {
                _masterDetailPage.IsPresented = false;
            }
        }

        public Task NavigateToMasterPage(ViewId masterId, ViewId? detailsId, bool animated = false)
        {
            return NavigateTo(masterId, animated);
        }

        public Task NavigateTo(ViewId viewId, KeyValuePair<string, object> viewModelParameter, bool animated = false)
        {
            //var viewModel = DependencyManager.Instance.Container.Resolve<BaseViewModel>(viewId.ToString(), viewModelParameter);

            //var page = DependencyManager.Instance.Container.Resolve<Page>(viewId.ToString(),
            //    new KeyValuePair<string, object>(nameof(viewModel), viewModel));

           // return NavigateToPage(viewId, animated, page);

            return Task.FromResult(true);
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Navigates to page.
        /// </summary>
        /// <param name="viewId">The view identifier.</param>
        /// <param name="animated">if set to <c>true</c> [animated].</param>
        /// <param name="page">The page.</param>
        /// <returns>Task</returns>
        private Task NavigateToPage(ViewId viewId, bool animated, Page page)
        {
            if (page is IBasePage mainPage)
            {
                mainPage.PageId = viewId;
            }

            return FormsNavigation.PushAsync(page, animated);
        }

        /// <summary>
        /// Sets the menu page.
        /// </summary>
        /// <param name="viewId">The view identifier.</param>
        private void SetMenuPage(ViewId viewId)
        {
            var page = DependencyManager.Instance.ServiceLocator.GetInstance<Page>(viewId.ToString());
            page.Title = AppResources.txtCRST;

            _masterDetailPage.Master = page;
        }

        #endregion
    }
}
