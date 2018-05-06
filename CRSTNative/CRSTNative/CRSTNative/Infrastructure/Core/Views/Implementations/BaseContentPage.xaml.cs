using DailyFitNative.Infrastructure.Core.ViewModels.Abstractions;
using DailyFitNative.Infrastructure.Core.Views.Abstractions;
using DailyFitNative.Utilities.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyFitNative.Infrastructure.Core.Views.Implementations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BaseContentPage : ContentPage, IBasePage
	{
	    #region Private Fields

	    protected readonly BaseViewModel _viewModel;

	    #endregion

        #region Constructors

        public BaseContentPage(BaseViewModel viewModel)
        {
            _viewModel = viewModel;
            BindingContext = viewModel;

	        InitializeComponent();
	    }

        #endregion

        #region IBasePage implementation

        public ViewId ViewId { get; set; }

        public virtual void OnPagePopped()
	    {
	        _viewModel.OnPagePopped();  
	    }

        #endregion
    }
}