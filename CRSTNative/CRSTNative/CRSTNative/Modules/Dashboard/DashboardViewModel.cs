using CRSTNative.Client.Infrastructure.Core.ViewModels.Abstractions;

namespace CRSTNative.Modules.Dashboard
{
    public class DashboardViewModel : BaseViewModel
    {
        #region Constractors

        public DashboardViewModel()
        {
        }

        #endregion

        #region Overrides Methods

        public override void OnPagePopped()
        {
            base.OnPagePopped();
        }

        protected override void OnAppearingExecute()
        {
            base.OnAppearingExecute();
        }

        protected override void OnDisappearingExecute()
        {
            base.OnDisappearingExecute();
        }

        #endregion
    }
}
