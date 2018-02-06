using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRSTNative.Client.Infrastructure.Core.Views.Implementations
{
    /// <summary>
    /// BaseMasterDetailPage
    /// </summary>
    /// <seealso cref="Xamarin.Forms.MasterDetailPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseMasterDetailPage : MasterDetailPage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMasterDetailPage"/> class.
        /// </summary>
        public BaseMasterDetailPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        #endregion
    }
}