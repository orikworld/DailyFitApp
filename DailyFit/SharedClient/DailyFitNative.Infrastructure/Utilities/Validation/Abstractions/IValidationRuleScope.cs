namespace DailyFitNative.Infrastructure.Utilities.Validation.Abstractions
{
	public interface IValidationRuleScope
	{
		void AddValidator(IValidator validator);

		void RemoveValidator(IValidator validator);
	}
}
