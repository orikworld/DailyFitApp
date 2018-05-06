using DailyFitNative.Infrastructure.Core.ViewModels.Abstractions;
using Xamarin.Forms.Xaml;
using BaseContentPage = DailyFitNative.Infrastructure.Core.Views.Implementations.BaseContentPage;

namespace DailyFitNative.Modules.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : BaseContentPage
	{
        public LoginPage(BaseViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}