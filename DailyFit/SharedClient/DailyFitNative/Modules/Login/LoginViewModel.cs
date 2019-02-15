using System.Windows.Input;
using DailyFitNative.Infrastructure.Core.ViewModels.Abstractions;
using DailyFitNative.Infrastructure.Utilities.Navigation;
using Xamarin.Forms;

namespace DailyFitNative.Modules.Login
{
    public class LoginViewModel : BaseViewModel
    {
		#region Properties

	    public string Login { get; set; }

	    public string Password { get; set; }

		#endregion

		public ICommand LoginCommand => new Command(LoginCommandExecute);

		#region Protected Methods

		public override  void Init()
		{
	

		}

		#endregion

		private void LoginCommandExecute()
        {
           NavigationService.Instance.SetRootPageAsunc(ViewId.MenuPage);
        }
	}
}
