using EventService.DTOs.Queries;
using EventService.Infrastructure.Models;
using EventService.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EventService.Infrastructure.Impl
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(EventDbContext dbContext) : base(dbContext)
        {        
        }

        public async Task<IEnumerable<EventAttendeesRatingDto>> GetEventAttendeesRatingAsync()
        {
            var query = _context.Events.Select(e => new EventAttendeesRatingDto
                            {
                                Title = e.Title,
                                InitDate = e.InitDate,
                                City = e.City,
                                NumAsistentes = _context.EventAttendees.Count(ea => ea.EventId == e.Id),
                                PromedioRating = _context.EventReviews.Where(er => er.EventId == e.Id).Any() 
                                                ? _context.EventReviews.Where(er => er.EventId == e.Id).Average(x => x.Rating)
                                                : 0
                            });

            return await query
                    .Where(dto => dto.NumAsistentes >= 10 && dto.PromedioRating > 3)
                    .OrderByDescending(dto => dto.PromedioRating)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetFilteredEventAsync(EventFilteredParams eventFilteredParams)
        {
            var query = _context.Events.Where(x => x.City == eventFilteredParams.City
                                                && x.Capacity > eventFilteredParams.MinCapacity
                                                && x.Price >= eventFilteredParams.MinPrice
                                                && x.Price <= eventFilteredParams.MaxPrice);
            
            return await query.OrderBy(x => x.InitDate)
                            .Skip((eventFilteredParams.PageNumber - 1) * eventFilteredParams.PageSize)
                            .Take(eventFilteredParams.PageSize)
                            .ToListAsync();

        }
    }
}