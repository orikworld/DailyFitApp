using System;

namespace DailyFitNative.Infrastructure.Utilities.Validation.Implementations
{
	public class PasswordMatchingRule 
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
