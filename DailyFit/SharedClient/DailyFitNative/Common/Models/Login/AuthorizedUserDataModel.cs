using DailyFitNative.Common.Enums;
using DailyFitNative.Common.Models.Licensing;

namespace DailyFitNative.Common.Models.Login
{
    public class AuthorizedUserDataModel
    {
	    public LoginNotificationType LoginNotificationType { get; set; }

		public UserInfoModel UserInfo { get; set; }

		public LicensingInfoModel LicensingInfo { get; set; }
	}
}
