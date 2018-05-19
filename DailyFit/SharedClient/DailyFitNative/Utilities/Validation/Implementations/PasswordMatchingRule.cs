using System;
using DailyFitNative.Utilities.Validation.Abstractions;

namespace DailyFitNative.Utilities.Validation.Implementations
{
	public class PasswordMatchingRule : IValidationRule<string>
	{
		#region Properties

		public string PasswordToCompare { get; set; }

		public string ValidationMessage { get; set; }

		#endregion

		#region Public Methods

		public bool Check(string value)
		{
			return !string.IsNullOrEmpty(value) && string.Equals(value, PasswordToCompare, StringComparison.Ordinal);
		}

		#endregion
	}
}
