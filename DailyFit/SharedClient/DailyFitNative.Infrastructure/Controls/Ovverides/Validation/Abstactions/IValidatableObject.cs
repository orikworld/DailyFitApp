using System;
using Xamarin.Forms;

namespace DailyFitNative.Infrastructure.Controls.Ovverides.Validation.Abstactions
{
	public interface IValidatableObject
	{
		void SetInvalidStyle(Style invalidStyle);

		void RestoreDefaultStyle();

		event EventHandler<FocusEventArgs> Unfocused;
	}
}
