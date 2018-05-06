using System.Windows.Input;
using DailyFitNative.Infrastructure.Core.ViewModels.Abstractions;
using DailyFitNative.Utilities.Navigation;
using Xamarin.Forms;

namespace DailyFitNative.Modules.Login
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand => new Command(LoginCommandExecute);

        private void LoginCommandExecute()
        {
           // NavigationService.Instance.SetRootPageAsunc(ViewId.MenuPage);
        }
    }
}
