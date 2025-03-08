using EventService.Models;

namespace EventService.Services
{
    public interface IAzureFunctionService
    {
        void CallEventCreatedFunctionAsync(Event newEvent);
    }
}