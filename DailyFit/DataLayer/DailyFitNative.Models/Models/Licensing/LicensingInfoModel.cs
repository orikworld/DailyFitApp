using System;

namespace DailyFitNative.Models.Models.Licensing
{
	public class LicensingInfoModel
	{
		public DateTime ExpirationDate { get; set; }

		public bool IsExpired { get; set; }
	}
}
