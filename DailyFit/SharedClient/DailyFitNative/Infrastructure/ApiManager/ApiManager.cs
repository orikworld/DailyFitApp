using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DailyFitNative.Common.Enums;
using DailyFitNative.Common.Models.Base.Abstractions;
using DailyFitNative.Common.Models.Base.Implementations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DailyFitNative.Infrastructure.ApiManager
{
	public class ApiManager : IApiManager
	{
		#region Fields

		private readonly HttpClient _client;

		#endregion

		#region Constructor

		public ApiManager()
		{
			_client = new HttpClient();
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiConstants.APPLICATION_JSON));
		}

		#endregion

		#region Properties

		public string BaseAddress
		{
			get => _client.BaseAddress.ToString();
			set => _client.BaseAddress = new Uri(value);
		}

		public string BearerToken { get; set; }

		#endregion

		#region Implementation of ApiManager

		public async Task<IJsonOperationResult<TResponse>> GetAsync<TResponse>(string uri)
		{
			SetAuthorizationToken();

			try
			{
				var response = await _client.GetAsync(new Uri(uri));
				return await DeserializeResponse<TResponse>(response);
			}
			catch (Exception e)
			{
				//todo: Log
			}

			return new JsonOperationResultGeneric<TResponse> { NotificationType = NotificationType.ApiNotAvailable };
		}

		public async Task<IJsonOperationResult<TResponse>> PostAsync<TResponse, TRequest>(string uri, TRequest request)
		{
			SetAuthorizationToken();

			var serializedRequst = JsonConvert.SerializeObject(request, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

			try
			{
				var response = await _client.PostAsync(new Uri(uri), new StringContent(serializedRequst, Encoding.UTF8, ApiConstants.APPLICATION_JSON));

				return await DeserializeResponse<TResponse>(response);
			}
			catch (Exception e)
			{
				//todo: Log
			}

			return new JsonOperationResultGeneric<TResponse> { NotificationType = NotificationType.ApiNotAvailable };
		}

		public async Task<IJsonOperationResult<TResponse>> PostAsync<TResponse>(string uri)
		{
			SetAuthorizationToken();
			try
			{
				var response = await _client.PostAsync(new Uri(uri), null);

				return await DeserializeResponse<TResponse>(response);
			}
			catch (Exception e)
			{
				//todo: Log
			}

			return new JsonOperationResultGeneric<TResponse> {NotificationType = NotificationType.ApiNotAvailable};
		}

		#endregion

		#region Public Methods

		public void SetAuthorizationToken()
		{
			_client.DefaultRequestHeaders.Authorization = BearerToken != null ? new AuthenticationHeaderValue(ApiConstants.BEARER_TOKEN, BearerToken) : null;
		}

		#endregion

		#region Private Methods

		private async Task<IJsonOperationResult<TResponse>> DeserializeResponse<TResponse>(HttpResponseMessage response)
		{
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				try
				{
					return JsonConvert.DeserializeObject<JsonOperationResultGeneric<TResponse>>(content);
				}
				catch (Exception e)
				{
					//todo: Log
				}
			}

			return new JsonOperationResultGeneric<TResponse>()
			{
				NotificationType = NotificationType.ServerErrorOccured
			};
		}

		#endregion
	}
}
