using System;
using Xamarin.Forms;

namespace DailyFitNative.Infrastructure.Controls.Ovverides.Validation.Abstractions
{
    public interface IFocusable
    {
	    event EventHandler<FocusEventArgs> Unfocused;

	    event EventHandler<FocusEventArgs> Focused;
	}
}
