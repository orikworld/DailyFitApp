using System;

namespace CRSTNative.Infrastructure.Localization
{
    /// <summary>
    /// Helper class for splitting locales like
    ///   iOS: ms_MY, gsw_CH
    ///   Android: in-ID
    /// into parts so we can create a .NET culture (or fallback culture)
    /// </summary>
    public class PlatformCulture
    {
        #region Constructors

        /// <summary>
        /// Initialize PlatformCulture information
        /// </summary>
        /// <param name="platformCultureString"></param>
        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier", nameof(platformCultureString));
            }

            PlatformString = platformCultureString.Replace("_", "-"); // .NET expects dash, not underscore

            var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);

            if (dashIndex > 0)
            {
                var parts = PlatformString.Split('-');

                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = string.Empty;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Represents platform specific culture string
        /// </summary>
        public string PlatformString { get; }

        /// <summary>
        /// Represents Language Code
        /// </summary>
        public string LanguageCode { get; }

        /// <summary>
        /// Represents Locale code
        /// </summary>
        public string LocaleCode { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Overrides ToString method to return only platform specific culture string
        /// </summary>
        /// <returns>Platform specific culture string</returns>
        public override string ToString() => PlatformString;

        #endregion
    }
}