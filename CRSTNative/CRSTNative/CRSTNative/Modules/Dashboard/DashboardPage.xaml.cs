using DailyFitNative.Infrastructure.Core.ViewModels.Abstractions;
using Xamarin.Forms.Xaml;
using BaseContentPage = DailyFitNative.Infrastructure.Core.Views.Implementations.BaseContentPage;

namespace DailyFitNative.Modules.Dashboard
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