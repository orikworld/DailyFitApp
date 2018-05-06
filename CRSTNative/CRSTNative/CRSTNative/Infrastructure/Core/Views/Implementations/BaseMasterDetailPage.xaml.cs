using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyFitNative.Infrastructure.Core.Views.Implementations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseMasterDetailPage : MasterDetailPage
    {
        #region Constructors

        public BaseMasterDetailPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        #endregion
    }
}