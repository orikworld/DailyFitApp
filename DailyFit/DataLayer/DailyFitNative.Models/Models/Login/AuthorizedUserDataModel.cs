using DailyFitNative.Models.Enums;
using DailyFitNative.Models.Models.Licensing;

namespace DailyFitNative.Models.Models.Login
{
    public class AuthorizedUserDataModel
    {
	    public LoginNotificationType LoginNotificationType { get; set; }

		public UserInfoModel UserInfo { get; set; }

		public LicensingInfoModel LicensingInfo { get; set; }
	}
}
