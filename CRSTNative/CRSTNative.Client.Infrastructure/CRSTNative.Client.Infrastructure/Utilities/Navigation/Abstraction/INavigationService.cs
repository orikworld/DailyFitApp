using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRSTNative.Client.Infrastructure.Utilities.Navigation.Abstraction
{
    /// <summary>
    /// INavigationService
    /// </summary>
    /// <seealso cref="Xamarin.Forms.INavigation" />
    public interface INavigationService : INavigation
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is last page.
        /// </summary>
        bool IsLastPage { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is main navigation stack.
        /// </summary>
        bool IsMainNavigationStack { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="viewId">The view identifier.</param>
        /// <param name="animated">if set to <c>true</c> [animated].</param>
        /// <returns></returns>
        Task NavigateTo(ViewId viewId, bool animated = false);

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="masterId">The master identifier.</param>
        /// <param name="detailsId">The details identifier.</param>
        /// <param name="animated">if set to <c>true</c> [animated].</param>
        Task NavigateToMasterPage(ViewId masterId, ViewId? detailsId, bool animated = false);

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="viewId">The view identifier.</param>
        /// <param name="animated">if set to <c>true</c> [animated].</param>
        /// <param name="viewModelParameter">View Model specific Parameter</param>
        /// <returns> An awaitable Task, indicating the Navigation completion. </returns>
        Task NavigateTo(ViewId viewId, KeyValuePair<string, object> viewModelParameter, bool animated = false);

        /// <summary>
        /// Navigates to modal.
        /// </summary>
        /// <param name="viewId">The view identifier.</param>
        /// <param name="animated">if set to <c>true</c> [animated].</param>
        /// <returns></returns>
        Task NavigateToModal(ViewId viewId, bool animated = false);

        /// <summary>
        /// Sets the root page asunc.
        /// </summary>
        /// <param name="viewId">The view identifier.</param>
        /// <param name="animated">if set to <c>true</c> [animated].</param>
        /// <returns></returns>
        Task SetRootPageAsunc(ViewId viewId, bool animated = false);

        /// <summary>
        /// Removes the page.
        /// </summary>
        /// <param name="viewId">The view identifier.</param>
        void RemovePage(ViewId viewId);

        /// <summary>
        /// Sets the root page.
        /// </summary>
        /// <param name="menuItemMenuPage">The menu item menu page.</param>
        void SetRootPage(Page menuItemMenuPage);

        /// <summary>
        /// Navigates from master page.
        /// </summary>
        void NavigateFromMasterPage();

        /// <summary>
        /// Hides the slider menu.
        /// </summary>
        void HideSliderMenu();

        #endregion

    }
}
