using System;
using DailyFitNative.Infrastructure.Utilities.Validation.Abstractions;

namespace DailyFitNative.Infrastructure.Utilities.Validation.RuleImplementations
{
	public class PasswordMatchingRule : IValidationRule<string>
	{
		#region Propeties

		public string PasswordToCompare { get; set; }

		public string ValidationMessage { get; set; }

		#endregion

		#region Public Methods

		public bool Check(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}

			return string.Equals(value, PasswordToCompare, StringComparison.Ordinal);
		}

		#endregion
	}
}
