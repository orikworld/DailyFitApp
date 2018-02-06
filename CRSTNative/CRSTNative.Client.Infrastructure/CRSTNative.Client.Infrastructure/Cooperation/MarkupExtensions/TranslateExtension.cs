using System;
using System.Collections.Generic;
using CRSTNative.General.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CRSTNative.Infrastructure.Resources;

namespace CRSTNative.Client.Infrastructure.Cooperation.MarkupExtensions
{

    /// <summary>
    /// Xaml translate extension provides resources accordingly to client culture
    /// </summary>
    [ContentProperty("ResourceKey")]
    public class TranslateExtension : IMarkupExtension
    {
        #region Properties

        /// <summary>
        /// Resources key
        /// </summary>
        public string ResourceKey { get; set; }

        #endregion

        #region Implementation of IMarkupExtension

        /// <summary>
        /// Returns the object created from the markup extension.
        /// </summary>
        /// <param name="serviceProvider">To be added.</param>
        /// <returns>The resource text by key</returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ResourceKey == null)
            {
                return string.Empty;
            }

            var translation = AppResources.ResourceManager.GetString(ResourceKey);

            if (translation == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessageConstants.KEY_WAS_NOT_FOUND_FOR_CULTURE, ResourceKey,
                    AppResources.Culture.Name));
            }

            return string.Empty;
        }

        #endregion
    }
}
