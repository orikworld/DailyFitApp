using CRSTNative.Client.Infrastructure.Core.ViewModels.Abstractions;
using CRSTNative.Client.Infrastructure.Core.Views.Implementations;
using Xamarin.Forms.Xaml;

namespace CRSTNative.Modules.Dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DashboardPage : BaseContentPage
	{
        public DashboardPage(BaseViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}