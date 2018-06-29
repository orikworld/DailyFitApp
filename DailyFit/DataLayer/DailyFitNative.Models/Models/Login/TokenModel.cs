using Newtonsoft.Json;

namespace DailyFitNative.Models.Models.Login
{
    public class TokenModel
    {
	    [JsonProperty("access_token")]
	    public string AccessToken { get; set; }

	    [JsonProperty("expires_in")]
	    public int ExpiresIn { get; set; }
	}
}
