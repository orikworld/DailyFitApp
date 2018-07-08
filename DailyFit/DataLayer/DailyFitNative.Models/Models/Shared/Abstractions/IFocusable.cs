using System;
using Xamarin.Forms;

namespace DailyFitNative.Models.Models.Base.Abstractions
{
    public interface IFocusable
    {
	    event EventHandler<FocusEventArgs> Unfocused;

	    event EventHandler<FocusEventArgs> Focused;
	}
}
