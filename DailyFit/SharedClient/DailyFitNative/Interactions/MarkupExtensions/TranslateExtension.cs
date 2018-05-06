using System;
using DailyFitNative.Common.Constants;
using DailyFitNative.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyFitNative.Interactions.MarkupExtensions
{
    [ContentProperty("ResourceKey")]
    public class TranslateExtension : IMarkupExtension
    {
        #region Properties

        public string ResourceKey { get; set; }

        #endregion

        #region Implementation of IMarkupExtension

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

            return translation;
        }

        #endregion
    }
}
