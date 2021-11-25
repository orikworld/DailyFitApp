using System.Collections.Generic;

namespace DailyFitNative.Infrastructure.Utilities.Validation.Abstractions
{
    public interface IValidatableObject<T>
	{
		#region Propeties

		IList<IValidationRule<T>> ValidationRules { get; }

		IList<string> Errors { get; set; }

		T Value { get; set; }

		bool IsValid { get; set; }

		#endregion

		#region Methods

		bool Validate();

		#endregion
	}
}
