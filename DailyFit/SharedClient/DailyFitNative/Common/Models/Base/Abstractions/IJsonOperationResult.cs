using DailyFitNative.Common.Enums;

namespace DailyFitNative.Common.Models.Base.Abstractions
{
    public interface IJsonOperationResult
    {
	    NotificationType NotificationType { get; set; }

	    string Message { get; set; }
	}
}
