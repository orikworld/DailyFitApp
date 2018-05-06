namespace DailyFitNative.Common.Enums
{
	public enum NotificationType : byte
	{
		Successful = 2,
		NotFound = 4,
		NotAuthenticated = 6,
		LicenseExpired = 8,
		RestrictedByLicense = 10,
		ServerErrorOccured = 12,
		DisplayedDataIsNotValid = 14,
		InternetConnectivityLost = 16,
		NotGranted = 18,
		ApiNotAvailable = 101,
	}
}
