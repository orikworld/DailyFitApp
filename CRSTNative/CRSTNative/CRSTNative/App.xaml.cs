using CommonServiceLocator;
using CRSTNative.AppStart;
using CRSTNative.Client.Infrastructure.Core.Views.Implementations;
using CRSTNative.Client.Infrastructure.Utilities.Navigation;
using CRSTNative.Infrastructure.DependencyInjection;
using CRSTNative.Modules.Dashboard;
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

		    var startingPage = DependencyManager.Instance.ServiceLocator.GetInstance<Page>(ViewId.LoginPage.ToString());
		    MainPage = new BaseNavigationPage(startingPage);

		    DependencyHelper.SetNavigationInstance(MainPage.Navigation);

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
