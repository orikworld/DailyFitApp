using System.Text.RegularExpressions;
using DailyFitNative.Infrastructure.Utilities.Validation.Abstractions;
using DailyFitNative.Models.Constants;

namespace DailyFitNative.Infrastructure.Utilities.Validation.RuleImplementations
{
	public class EmailRule : IValidationRule<string>
	{
		#region Properties

		public string ValidationMessage { get; set; }

		#endregion

		#region Public Methods

		public bool Check(string value)
		{
			if (value == null)
			{
				return false;
			}
			var regex = new Regex(CommonConstants.EMAIL_REGEX);

			var match = regex.Match(value);

			return match.Success;
		}

		#endregion
	}
}
