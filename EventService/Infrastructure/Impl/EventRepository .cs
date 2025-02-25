using EventService.Models;

namespace EventService.Infrastructure.Impl
{
    public class EventRepository : GenericRepository<Event>
    {
        public EventRepository(EventDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}