using CommonServiceLocator;
using DailyFitNative.AppStart;
using DailyFitNative.Infrastructure.Core.Views.Abstractions;
using DailyFitNative.Infrastructure.Core.Views.Implementations;
using DailyFitNative.Infrastructure.DependencyInjection;
using DailyFitNative.Infrastructure.Utilities.Navigation;
using DailyFitNative.Models.Constants;
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
		}

		#endregion

		#region Protected Methods

		protected override void OnStart ()
		{
			StartAppCenter();

			SetStartingPage();
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

		private void SetStartingPage()
		{
			var startingPage = DependencyManager.Instance.ServiceLocator.GetInstance<Page>(ViewId.LoginPage.ToString());

			if (startingPage is IBasePage basePage)
			{
				basePage.ViewId = ViewId.LoginPage;
				basePage.Init();
			}

			MainPage = new BaseNavigationPage(startingPage);

			DependencyHelper.SetNavigationInstance(MainPage.Navigation);
		}

		private void StartAppCenter()
		{
			AppCenter.Start($"{CommonConstants.APP_CENTER_DROID_KEY}{CommonConstants.APP_CENTER_IOS_KEY}", typeof(Analytics), typeof(Crashes));
		}

		#endregion
	}
}
