using CRSTNative.Client.Infrastructure.Core.ViewModels.Abstractions;
using CRSTNative.Client.Infrastructure.Core.Views.Abstractions;
using CRSTNative.Client.Infrastructure.Utilities.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRSTNative.Client.Infrastructure.Core.Views.Implementations
{
    /// <summary>
    /// BaseContentPage
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    /// <seealso cref="CRSTNative.Client.Infrastructure.Core.Views.Abstractions.IBasePage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BaseContentPage : ContentPage, IBasePage
	{
	    #region Private Fields

	    protected readonly BaseViewModel _viewModel;

	    #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseContentPage"/> class.
        /// </summary>
        public BaseContentPage(BaseViewModel viewModel)
        {
            _viewModel = viewModel;
            BindingContext = viewModel;

	        InitializeComponent();
	    }

        #endregion

        #region IBasePage implementation

        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        public ViewId PageId { get; set; }

        /// <summary>
        /// Called when [page popped].
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void OnPagePopped()
	    {
	        _viewModel.OnPagePopped();  
	    }

        #endregion
    }
}