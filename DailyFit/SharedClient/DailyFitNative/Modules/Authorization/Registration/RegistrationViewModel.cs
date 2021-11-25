using System;
using System.Windows.Input;
using DailyFitNative.Infrastructure.Core.ViewModels.Abstractions;
using DailyFitNative.Infrastructure.Resources;
using DailyFitNative.Infrastructure.Utilities.Navigation;
using DailyFitNative.Infrastructure.Utilities.Validation.Abstractions;
using DailyFitNative.Infrastructure.Utilities.Validation.Implementations;
using DailyFitNative.Infrastructure.Utilities.Validation.RuleImplementations;
using Xamarin.Forms;

namespace DailyFitNative.Modules.Authorization.Registration
{
    public class RegistrationViewModel : BaseViewModel
    {
        #region Private Fields

        private IValidatableObject<string> _login;

        private IValidatableObject<string> _password;

        private IValidatableObject<string> _confirmPassword;

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

        public IValidatableObject<string> ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public ICommand RegisterCommand => new Command(RegisterExecute);

        public bool IsEnabledRegister
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

        public RegistrationViewModel()
        {

        }

        #endregion

        #region Protected Methods

        public override void Init()
        {
            Login = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            ConfirmPassword = new ValidatableObject<string>();
            AddValidations();
        }

        #endregion

        #region Private Methods

        private void RegisterExecute()
        {
            NavigationService.Instance.NavigateTo(ViewId.LoginPage);
        }

        private void CheckLoginEnabledExecute()
        {
            SendPropertyChanged(() => IsEnabledRegister);
        }

        private void AddValidations()
        {
            Login.ValidationRules.Add(new IsNotNullOrEmptyRule { ValidationMessage = AppResources.msgEmptyMail });
            Login.ValidationRules.Add(new EmailRule { ValidationMessage = AppResources.msgInvalidEmail });
            Password.ValidationRules.Add(new IsNotNullOrEmptyRule { ValidationMessage = AppResources.msgEmptyPassword });
            ConfirmPassword.ValidationRules.Add(new IsNotNullOrEmptyRule { ValidationMessage = AppResources.msgEmptyConfirmPassword });

        }

        #endregion
    }
}
