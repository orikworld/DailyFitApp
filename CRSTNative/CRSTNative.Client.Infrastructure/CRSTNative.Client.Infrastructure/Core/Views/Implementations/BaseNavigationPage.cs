
using System;
using CRSTNative.Client.Infrastructure.Core.Views.Abstractions;
using CRSTNative.Client.Infrastructure.Utilities.Navigation;
using Xamarin.Forms;

namespace CRSTNative.Client.Infrastructure.Core.Views.Implementations
{
    /// <summary>
    /// BaseNavigationPage
    /// </summary>
    /// <seealso cref="Xamarin.Forms.NavigationPage" />
    public class BaseNavigationPage : NavigationPage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseNavigationPage"/> class.
        /// </summary>
        /// <param name="root">The root.</param>
        public BaseNavigationPage(Page root) : base(root)
        {
            Popped += OnPopped;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Called when [popped].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="navigationEventArgs">The <see cref="Xamarin.Forms.NavigationEventArgs" /> instance containing the event data.</param>
        private void OnPopped(object sender, NavigationEventArgs navigationEventArgs)
        {
            (navigationEventArgs.Page as IBasePage)?.OnPagePopped();

            if (navigationEventArgs.Page is BaseMasterDetailPage)
            {
                //todo need implement method
                NavigationService.Instance.NavigateFromMasterPage();
            }
        }

        #endregion
    }
}
