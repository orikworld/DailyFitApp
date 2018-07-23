using DailyFitNative.Infrastructure.Core.ViewModels.Abstractions;
using DailyFitNative.Infrastructure.Core.Views.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyFitNative.Modules.Authorization.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : BaseContentPage
    {
        public RegistrationPage(BaseViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
