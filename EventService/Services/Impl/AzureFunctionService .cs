using EventService.Constants;
using EventService.Models;

namespace EventService.Services.Impl
{
    public class AzureFunctionService : IAzureFunctionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _functionUrl;

        public AzureFunctionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var functionConfig = configuration.GetSection("AzureFunction");
            _functionUrl = $"{functionConfig["BaseUrl"]}/{AzureFunctionConstants.CREATE_EVENT}";
        }

        public async Task<bool> CallEventCreatedFunctionAsync(Event newEvent)
        {
            var response = await _httpClient.PostAsJsonAsync(_functionUrl, newEvent);
            return response.IsSuccessStatusCode;
        }
    }
}