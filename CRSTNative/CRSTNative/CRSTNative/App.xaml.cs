using CommonServiceLocator;
using CRSTNative.AppStart;
using CRSTNative.Client.Infrastructure.Core.Views.Implementations;
using CRSTNative.Client.Infrastructure.Utilities.Navigation;
using CRSTNative.Infrastructure.DependencyInjection;
using Xamarin.Forms;

namespace CRSTNative
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
		    DependencyHelper.SetDependencies();
		    ServiceLocator.SetLocatorProvider(() => DependencyManager.Instance.ServiceLocator);

		    Page startingPage = null;

		    startingPage = DependencyManager.Instance.ServiceLocator.GetInstance<Page>(ViewId.MenuPage.ToString());
		    MainPage = new BaseNavigationPage(startingPage);

        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
