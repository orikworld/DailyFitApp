using System;
using System.Collections.Generic;
using System.Text;
using CRSTNative.Client.Infrastructure.Core.ViewModels.Abstractions;

namespace CRSTNative.Modules.Menu
{
    public class MenuViewModel : BaseViewModel
    {
        #region Constractors

        public MenuViewModel()
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
