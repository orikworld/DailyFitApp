using DailyFitNative.Models.Models.Base.Abstractions;
using Xamarin.Forms;

namespace DailyFitNative.Models.Models.Shared.Abstractions
{
   public interface IValidationControl : IFocusable
	{
	    Color BorderColor { get; set; }
	}
}
