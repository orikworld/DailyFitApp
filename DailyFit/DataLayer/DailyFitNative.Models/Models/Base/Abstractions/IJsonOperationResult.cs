using DailyFitNative.Models.Enums;

namespace DailyFitNative.Models.Models.Base.Abstractions
{
    public interface IJsonOperationResult
    {
	    NotificationType NotificationType { get; set; }

	    string Message { get; set; }
	}
}
