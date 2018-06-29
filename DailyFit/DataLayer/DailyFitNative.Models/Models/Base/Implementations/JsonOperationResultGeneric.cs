using System.Collections.Generic;
using DailyFitNative.Models.Enums;
using DailyFitNative.Models.Models.Base.Abstractions;

namespace DailyFitNative.Models.Models.Base.Implementations
{
	public class JsonOperationResultGeneric<T> : IJsonOperationResult<T>
	{
		#region Constructors

		public JsonOperationResultGeneric()
		{
		}

		public JsonOperationResultGeneric(T item) => Item = item;

		public JsonOperationResultGeneric(IEnumerable<T> items) => Items = items;

		#endregion

		#region Public properties

		public IEnumerable<T> Items { get; set; }

		public T Item { get; set; }

		public NotificationType NotificationType { get; set; }

		public string Message { get; set; }

		#endregion
	}
}
