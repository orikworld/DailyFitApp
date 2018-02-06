using CRSTNative.Client.Infrastructure.Core.ViewModels.Abstractions;
using CRSTNative.Client.Infrastructure.Core.Views.Implementations;
using Xamarin.Forms.Xaml;

namespace CRSTNative.Modules.Menu
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