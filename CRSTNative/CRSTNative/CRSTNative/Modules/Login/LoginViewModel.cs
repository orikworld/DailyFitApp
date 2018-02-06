using System.Windows.Input;
using CRSTNative.Client.Infrastructure.Core.ViewModels.Abstractions;
using CRSTNative.Client.Infrastructure.Utilities.Navigation;
using Xamarin.Forms;

namespace CRSTNative.Modules.Login
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand => new Command(LoginCommandExecute);

        private void LoginCommandExecute()
        {
            NavigationService.Instance.SetRootPageAsunc(ViewId.MenuPage);
        }
    }
}
