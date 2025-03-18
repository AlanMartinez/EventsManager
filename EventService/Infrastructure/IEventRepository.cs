using EventService.DTOs.Queries;
using EventService.Infrastructure.Models;
using EventService.Models;

namespace EventService.Infrastructure
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<IEnumerable<EventAttendeesRatingDto>> GetEventAttendeesRatingAsync();

        Task<IEnumerable<Event>> GetFilteredEventAsync(EventFilteredParams eventFilteredParams);
    }
}