using EventService.Models;

namespace EventService.Services
{
    public interface IAzureFunctionService
    {
        Task<bool> CallEventCreatedFunctionAsync(Event newEvent);
    }
}