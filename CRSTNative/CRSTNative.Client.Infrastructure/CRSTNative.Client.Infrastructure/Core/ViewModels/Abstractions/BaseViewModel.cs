using System.Windows.Input;
using CRSTNative.Client.Infrastructure.Core.ViewModels.Implementations;
using Xamarin.Forms;

namespace CRSTNative.Client.Infrastructure.Core.ViewModels.Abstractions
{
    /// <summary>
    /// BaseViewModel
    /// </summary>
    /// <seealso cref="CRSTNative.Client.Infrastructure.Core.ViewModels.Implementations.NotifyPropertyChangedImplementation" />
    public abstract class BaseViewModel : NotifyPropertyChangedImplementation
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        protected BaseViewModel() { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the on appearing command.
        /// </summary>
        public ICommand OnAppearingCommand => new Command(OnAppearingExecute);

        /// <summary>
        /// Gets the on disappearing command.
        /// </summary>
        public ICommand OnDisappearingCommand => new Command(OnDisappearingExecute);

        #endregion

        #region Virtual Methods

        protected virtual void OnAppearingExecute() { }

        protected virtual void OnDisappearingExecute() { }

        public virtual void OnPagePopped() { }

        #endregion
    }
}
