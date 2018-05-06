using System;
using System.Collections.Generic;
using System.Text;
using DailyFitNative.Common.Enums;
namespace DailyFitNative.Common.Models.Login
{
    public class UserInfoModel
    {
		public Guid UserPK { get; set; }

	    public UserType UserType {get; set; }

		public bool IsActivated { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string FullName => $"{FirstName} {LastName}";

		public string Message { get; set; }

	    public string ProfileImageUrl { get; set; }
	}
}
