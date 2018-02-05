using CRSTNative.Client.Infrastructure.Utilities.Navigation;

namespace CRSTNative.Client.Infrastructure.Core.Views.Abstractions
{
    /// <summary>
    /// IBasePage
    /// </summary>
    public interface IBasePage
    {
        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        ViewId PageId { get; set; }

        /// <summary>
        /// Called when [page popped].
        /// </summary>
        void OnPagePopped();
    }
}
