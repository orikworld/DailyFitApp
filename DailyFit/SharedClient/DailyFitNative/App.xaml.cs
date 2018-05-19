using CommonServiceLocator;
using DailyFitNative.AppStart;
using DailyFitNative.Common.Constants;
using DailyFitNative.Infrastructure.Core.Views.Implementations;
using DailyFitNative.Infrastructure.DependencyInjection;
using DailyFitNative.Utilities.Navigation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace DailyFitNative
{
	public partial class App : Application
	{
		#region Constructors

		public App ()
		{
			InitializeComponent();
			DependencyHelper.SetDependencies();

			ServiceLocator.SetLocatorProvider(() => DependencyManager.Instance.ServiceLocator);

			var startingPage = DependencyManager.Instance.ServiceLocator.GetInstance<Page>(ViewId.LoginPage.ToString());
			MainPage = new BaseNavigationPage(startingPage);

			DependencyHelper.SetNavigationInstance(MainPage.Navigation);
		}

		#endregion

		#region Protected Methods

		protected override void OnStart ()
		{
			StartAppCenter();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

		#endregion

		#region Private Methods

		private void StartAppCenter()
		{
			AppCenter.Start($"{CommonConstants.APP_CENTER_DROID_KEY}{CommonConstants.APP_CENTER_IOS_KEY}", typeof(Analytics), typeof(Crashes));
		}

		#endregion
	}
}
