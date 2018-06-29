using DailyFitNative.Infrastructure.Interactions.Behaviors.Validation.Abstactions;
using EntryWithFrameBorder = DailyFitNative.Infrastructure.Controls.Views.EntryWithFrameBorder;

namespace DailyFitNative.Infrastructure.Interactions.Behaviors.Validation.Implemantations
{
	public class EmptyEntryValidationRuleBehavior : ValidationRuleBehavior<EntryWithFrameBorder>
	{
		#region Implementations of ValidationRuleBehavior

		public override bool Validate()
		{
			var isValid = !string.IsNullOrWhiteSpace(AssociatedObject.Text);

			NotifyValidationContainer(isValid);

			return isValid;
		}

		#endregion
	}
}
