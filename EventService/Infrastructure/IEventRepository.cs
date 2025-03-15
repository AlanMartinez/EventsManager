using EventService.DTOs.Queries;
using EventService.Models;

namespace EventService.Infrastructure
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<IEnumerable<EventAttendeesRatingDto>> GetEventAttendeesRatingDtoAsync();
    }
}