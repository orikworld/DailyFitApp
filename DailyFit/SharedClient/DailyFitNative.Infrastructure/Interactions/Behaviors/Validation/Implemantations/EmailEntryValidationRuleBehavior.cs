using System.Text.RegularExpressions;
using DailyFitNative.Infrastructure.Interactions.Behaviors.Validation.Abstactions;
using DailyFitNative.Models.Constants;
using EntryWithFrameBorder = DailyFitNative.Infrastructure.Controls.Views.EntryWithFrameBorder;

namespace DailyFitNative.Infrastructure.Interactions.Behaviors.Validation.Implemantations
{
	public class EmailEntryValidationRuleBehavior : ValidationRuleBehavior<EntryWithFrameBorder>
	{
		#region Implementations of ValidationRuleBehavior

		public override bool Validate()
		{
			var regex = new Regex(CommonConstants.EMAIL_REGEX);

			var match = regex.Match(AssociatedObject.Text);

			var isValid = match.Success;

			NotifyValidationContainer(isValid);

			return isValid;
		}

		#endregion
	}
}
