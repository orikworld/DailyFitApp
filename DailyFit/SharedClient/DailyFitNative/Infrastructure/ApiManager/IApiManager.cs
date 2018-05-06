using System.Threading.Tasks;
using DailyFitNative.Common.Models.Base.Abstractions;

namespace DailyFitNative.Infrastructure.ApiManager
{
    public interface IApiManager
    {
	    #region Properties

	    string BaseAddress { get; set; }

	    string BearerToken { get; set; }

	    #endregion

	    #region Methods

	    Task<IJsonOperationResult<TResponse>> GetAsync<TResponse>(string uri);

	    Task<IJsonOperationResult<TResponse>> PostAsync<TResponse, TRequest>(string uri, TRequest request);

	    Task<IJsonOperationResult<TResponse>> PostAsync<TResponse>(string uri);

	    #endregion
	}
}
