using CRSTNative.Client.Infrastructure.Core.ViewModels.Abstractions;
using CRSTNative.Client.Infrastructure.Core.Views.Implementations;
using Xamarin.Forms.Xaml;

namespace CRSTNative.Modules.Login
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