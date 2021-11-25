using System.Windows.Input;
using DailyFitNative.Infrastructure.Core.ViewModels.Implementations;
using DailyFitNative.Infrastructure.Utilities.Validation.Implementations;
using Xamarin.Forms;

namespace DailyFitNative.Infrastructure.Core.ViewModels.Abstractions
{
    public abstract class BaseViewModel : NotifyPropertyChangedImplementation
    {
        #region Properties

        public ICommand OnAppearingCommand => new Command(OnAppearingExecute);

        public ICommand OnDisappearingCommand => new Command(OnDisappearingExecute);

		#endregion

	    #region Constructors

	    protected BaseViewModel()
	    {
			
	    }

	    #endregion

		#region Virtual Methods

		protected virtual void OnAppearingExecute() { }

        protected virtual void OnDisappearingExecute() { }

        public virtual void OnPagePopped() { }

	    public virtual void Init() { }

		#endregion

	    #region Public Methods

	    public bool ValidateObject<T>(ValidatableObject<T> validatableObject)
	    {
			//this will be extended
		    return !validatableObject.Validate();
	    }

	    #endregion
	}
}
