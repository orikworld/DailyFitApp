using System.Collections.Generic;

namespace DailyFitNative.Models.Models.Base.Abstractions
{
	public interface IJsonOperationResult<T> : IJsonOperationResult
	{
		IEnumerable<T> Items { get; set; }

		T Item { get; set; }
	}
}
