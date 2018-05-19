using System.Text.RegularExpressions;
using DailyFitNative.Common.Constants;
using DailyFitNative.Utilities.Validation.Abstractions;

namespace DailyFitNative.Utilities.Validation.Implementations
{
	public class EmailRule<T> : IValidationRule<T>
	{
		#region Properties

		public string ValidationMessage { get; set; }

		#endregion

		#region Public Methods

		public bool Check(T value)
		{
			if (value != null)
			{
				var str = value as string;

				var regex = new Regex(CommonConstants.EMAIL_REGEX);

				var match = regex.Match(str);

				return match.Success;
			}

			return false;
		}
		#endregion

	}
}
