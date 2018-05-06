using CommonServiceLocator;
using DailyFitNative.AppStart;
using DailyFitNative.Infrastructure.Core.Views.Implementations;
using DailyFitNative.Infrastructure.DependencyInjection;
using DailyFitNative.Utilities.Navigation;
using Xamarin.Forms;

namespace DailyFitNative
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
