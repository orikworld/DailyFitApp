namespace DailyFitNative.Utilities.Validation.Abstractions
{
    public interface IValidationRule<in T>
	{
		#region Properties

		string ValidationMessage { get; set; }

		#endregion

		#region Methods

		bool Check(T value);

		#endregion
	}
}
