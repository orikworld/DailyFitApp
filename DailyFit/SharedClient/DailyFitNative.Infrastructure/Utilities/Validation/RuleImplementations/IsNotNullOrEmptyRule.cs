using DailyFitNative.Infrastructure.Utilities.Validation.Abstractions;

namespace DailyFitNative.Infrastructure.Utilities.Validation.RuleImplementations
{
	public class IsNotNullOrEmptyRule : IValidationRule<string>
	{
		#region Properties

		public string ValidationMessage { get; set; }

		#endregion

		#region Public Methods

		public bool Check(string value)
		{
			return !string.IsNullOrWhiteSpace(value);
		}

		#endregion
	}
}
