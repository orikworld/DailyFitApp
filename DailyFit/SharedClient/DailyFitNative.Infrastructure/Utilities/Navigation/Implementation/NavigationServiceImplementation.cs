using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyFitNative.Infrastructure.Core.Views.Abstractions;
using DailyFitNative.Infrastructure.Core.Views.Implementations;
using DailyFitNative.Infrastructure.DependencyInjection;
using DailyFitNative.Infrastructure.Resources;
using DailyFitNative.Infrastructure.Utilities.Navigation.Abstraction;
using Xamarin.Forms;
using BaseMasterDetailPage = DailyFitNative.Infrastructure.Core.Views.Implementations.BaseMasterDetailPage;

namespace DailyFitNative.Infrastructure.Utilities.Navigation.Implementation
{

	public class NavigationServiceImplementation : INavigationService
    {
        #region Private Fields

        private MasterDetailPage _masterDetailPage;

        #endregion

        #region Constructors

        public NavigationServiceImplementation(INavigation navigation)
        {
            FormsNavigation = navigation;
            ModalStack = navigation.ModalStack;
            NavigationStack = navigation.NavigationStack;
        }

        #endregion

        #region Properties

        public IReadOnlyList<Page> ModalStack { get; private set; }

        public IReadOnlyList<Page> NavigationStack { get; private set; }

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

        // toDo do not use this method before issue "https://bugzilla.xamarin.com/show_bug.cgi?id=53179" is fixed and android package update. Use Pop instead
        public void RemovePage(Page page)
        {
            (page as IBasePage)?.OnPagePopped();
            FormsNavigation.RemovePage(page);
        }

        public Task NavigateTo(ViewId viewId, bool animated = false)
        {
            var page = DependencyManager.Instance.ServiceLocator.GetInstance<Page>(viewId.ToString());

            if (page is IBasePage basePage)
            {
	            basePage.Init();
	            basePage.ViewId = viewId;
            }

            return PushAsync(page, animated);
        }

        public Task NavigateToModal(ViewId viewId, bool animated = false)
        {
            var page = DependencyManager.Instance.ServiceLocator.GetInstance<Page>(viewId.ToString());

            if (page is IBasePage basePage)
            {
	            basePage.Init();
	            basePage.ViewId = viewId;
            }

            return PushModalAsync(page, animated);
        }

        public Task SetRootPageAsunc(ViewId viewId, bool animated = false)
        {
            if (viewId == ViewId.MenuPage)
            {
	            var detailPage = new BaseNavigationPage(
		            DependencyManager.Instance.ServiceLocator.GetInstance<Page>(ViewId.DashboardPage.ToString()));

	            if (detailPage is IBasePage basePage)
	            {
		            basePage.ViewId = ViewId.DashboardPage;
					basePage.Init();
	            }

				_masterDetailPage = new BaseMasterDetailPage()
                {
                    Detail = detailPage,

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

        /// toDo do not use this method before issue "https://bugzilla.xamarin.com/show_bug.cgi?id=53179" is fixed and android package update. Use Pop instead
        public void RemovePage(ViewId viewId)
        {
            var pageToRemove = NavigationStack.FirstOrDefault(i => (i as IBasePage)?.ViewId == viewId) ??
                               ModalStack.FirstOrDefault(i => (i as IBasePage)?.ViewId == viewId);

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

        public void HideSliderMenu()
        {
            if (_masterDetailPage != null)
            {
                _masterDetailPage.IsPresented = false;
            }
        }

		#endregion

		#region Private Methods

		private void SetMenuPage(ViewId viewId)
        {
            var page = DependencyManager.Instance.ServiceLocator.GetInstance<Page>(viewId.ToString());

            page.Title = AppResources.txtAppName;

	        if (page is IBasePage basePage)
	        {
		        basePage.Init();
		        basePage.ViewId = viewId;
	        }

            _masterDetailPage.Master = page;
        }

        #endregion
    }
}
