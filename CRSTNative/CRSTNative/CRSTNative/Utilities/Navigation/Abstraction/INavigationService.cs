﻿using System.Threading.Tasks;
using Xamarin.Forms;

namespace DailyFitNative.Utilities.Navigation.Abstraction
{
    public interface INavigationService : INavigation
    {
        #region Properties

        bool IsLastPage { get; }

        bool IsMainNavigationStack { get; }

        #endregion

        #region Methods

        Task NavigateTo(ViewId viewId, bool animated = false);

        Task NavigateToMasterPage(ViewId masterId, ViewId? detailsId, bool animated = false);

        Task NavigateToModal(ViewId viewId, bool animated = false);

        Task SetRootPageAsunc(ViewId viewId, bool animated = false);

        void RemovePage(ViewId viewId);

        void SetRootPage(Page menuItemMenuPage);

        void HideSliderMenu();

        #endregion

    }
}
