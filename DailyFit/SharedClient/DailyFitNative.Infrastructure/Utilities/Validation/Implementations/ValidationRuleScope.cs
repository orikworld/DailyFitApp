using System.Collections.Generic;
using System.Linq;
using DailyFitNative.Infrastructure.Utilities.Validation.Abstractions;

namespace DailyFitNative.Infrastructure.Utilities.Validation.Implementations
{
	public class ValidationRuleScope : IValidationRuleScope, IValidator
	{
		#region Private Fields

		private readonly IList<IValidator> _validators = new List<IValidator>();

		#endregion

		#region Implementation of IValidationRuleScope

		public void AddValidator(IValidator validator) => _validators.Add(validator);

		public void RemoveValidator(IValidator validator) => _validators.Remove(validator);

		#endregion

		#region Implementation of IValidator

		public bool Validate() => _validators.Aggregate(true, (current, validator) => validator.Validate() && current);

		#endregion
	}
}