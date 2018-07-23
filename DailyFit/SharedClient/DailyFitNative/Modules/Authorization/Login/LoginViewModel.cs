using System.Windows.Input;
using DailyFitNative.Infrastructure.Core.ViewModels.Abstractions;
using DailyFitNative.Infrastructure.Resources;
using DailyFitNative.Infrastructure.Utilities.Navigation;
using DailyFitNative.Infrastructure.Utilities.Validation.Abstractions;
using DailyFitNative.Infrastructure.Utilities.Validation.Implementations;
using DailyFitNative.Infrastructure.Utilities.Validation.RuleImplementations;
using Xamarin.Forms;

namespace DailyFitNative.Modules.Authorization.Login
{
	public class LoginViewModel : BaseViewModel
	{
		#region Private Fields

		private IValidatableObject<string> _login;

		private IValidatableObject<string> _password;

		#endregion

		#region Properties

		public IValidatableObject<string> Login
		{
			get => _login;
			set => SetProperty(ref _login, value);
		}

		public IValidatableObject<string> Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		public ICommand LoginCommand => new Command(LoginExecute);

		public ICommand CheckLoginEnabledCommand => new Command(CheckLoginEnabledExecute);

		public bool IsEnabledLogin
		{
			get
			{
				if (Login != null && Password != null)
				{
					return Login.IsValid && Password.IsValid;
				}

				return false;
			}
		}
		#endregion

		#region Constructors

		public LoginViewModel()
		{
	
		}

		#endregion

		#region Protected Methods

		public override void Init()
		{
			Login = new ValidatableObject<string>();
			Password = new ValidatableObject<string>();
			AddValidations();
		}

		#endregion

		#region Private Methods

		private void LoginExecute()
		{
            NavigationService.Instance.NavigateTo(ViewId.RegistrationPage);
		}

		private void CheckLoginEnabledExecute()
		{
			SendPropertyChanged(() => IsEnabledLogin);
		}

		private void AddValidations()
		{
			Login.ValidationRules.Add(new IsNotNullOrEmptyRule { ValidationMessage = AppResources.msgEmptyMail});
			Login.ValidationRules.Add(new EmailRule { ValidationMessage = AppResources.msgInvalidEmail });
			Password.ValidationRules.Add(new IsNotNullOrEmptyRule{ ValidationMessage = AppResources.msgEmptyPassword});
		}

		#endregion
	}
}
