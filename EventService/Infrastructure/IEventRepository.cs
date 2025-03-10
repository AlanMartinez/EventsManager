using EventService.DTOs.Queries;

namespace EventService.Infrastructure
{
    public interface IEventRepository
    {
        Task<IEnumerable<EventAttendeesRatingDto>> GetEventAttendeesRatingDtoAsync();
    }
}
