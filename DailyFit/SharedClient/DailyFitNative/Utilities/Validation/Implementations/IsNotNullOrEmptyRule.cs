using DailyFitNative.Utilities.Validation.Abstractions;

namespace DailyFitNative.Utilities.Validation.Implementations
{
	public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
	{
		#region Properties

		public string ValidationMessage { get; set; }

		#endregion

		#region Public Methods

		public bool Check(T value)
		{
			if (value == null)
			{
				return false;
			}

			var str = value as string;

			return !string.IsNullOrWhiteSpace(str);
		}

		#endregion
	}
}
