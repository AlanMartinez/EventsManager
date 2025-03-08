using AutoMapper;
using EventService.Constants;
using EventService.DTOs.AzureFunctions;
using EventService.Models;

namespace EventService.Services.Impl
{
    public class AzureFunctionService : IAzureFunctionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _functionUrl;
        private readonly IMapper _mapper;
        private readonly ILogger<AzureFunctionService> _logger;

        public AzureFunctionService(HttpClient httpClient, IConfiguration configuration, IMapper mapper, ILogger<AzureFunctionService> logger)
        {
            _httpClient = httpClient;
            var functionConfig = configuration.GetSection("AzureFunction");
            _functionUrl = $"{functionConfig["BaseUrl"]}/{AzureFunctionConstants.CREATE_EVENT}";
            _mapper = mapper;
            _logger = logger;
        }

        public async void CallEventCreatedFunctionAsync(Event newEvent)
        {
            var createdEvent = _mapper.Map<EventCreatedFunctionDto>(newEvent);
            var json = System.Text.Json.JsonSerializer.Serialize(createdEvent);
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_functionUrl, json);
            } catch (Exception)
            {
                _logger.LogError($"No se pudo ejecutar correctamente {AzureFunctionConstants.CREATE_EVENT}");
            }
        }
    }
}