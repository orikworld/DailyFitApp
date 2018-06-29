using DailyFitNative.Infrastructure.Core.ViewModels.Abstractions;
using DailyFitNative.Infrastructure.Core.Views.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyFitNative.Modules.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : BaseContentPage
	{ 
        public LoginPage(BaseViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}