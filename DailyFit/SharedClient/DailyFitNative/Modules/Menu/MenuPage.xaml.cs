using DailyFitNative.Infrastructure.Core.ViewModels.Abstractions;
using Xamarin.Forms.Xaml;
using BaseContentPage = DailyFitNative.Infrastructure.Core.Views.Implementations.BaseContentPage;

namespace DailyFitNative.Modules.Menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : BaseContentPage
	{
		public MenuPage (BaseViewModel viewModel) : base(viewModel)
		{
			InitializeComponent ();
		}
	}
}