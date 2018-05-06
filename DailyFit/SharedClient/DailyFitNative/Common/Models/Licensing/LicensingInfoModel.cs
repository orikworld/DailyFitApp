using System;

namespace DailyFitNative.Common.Models.Licensing
{
	public class LicensingInfoModel
	{
		public DateTime ExpirationDate { get; set; }

		public bool IsExpired { get; set; }
	}
}
