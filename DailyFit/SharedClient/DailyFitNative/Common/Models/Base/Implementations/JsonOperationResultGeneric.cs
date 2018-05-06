using System.Collections.Generic;
using DailyFitNative.Common.Enums;
using DailyFitNative.Common.Models.Base.Abstractions;

namespace DailyFitNative.Common.Models.Base.Implementations
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
