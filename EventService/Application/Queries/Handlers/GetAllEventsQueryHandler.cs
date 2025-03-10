using EventService.Infrastructure;
using EventService.Models;
using EventService.Services;
using MediatR;

namespace EventService.Application.Queries.Handlers
{
    public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, IEnumerable<Event>>
    {
        private readonly IGenericRepository<Event> _repository;
        private readonly ICacheService<Event> _cacheService;

        public GetAllEventsQueryHandler(IGenericRepository<Event> repository, ICacheService<Event> cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var cachedEvents = await _cacheService.GetCachedEventsAsync();
            if (cachedEvents != null && cachedEvents.Any())
                return cachedEvents;
            

            var events = await _repository.GetAllAsync();

            await _cacheService.SetCachedEventsAsync(events);

            return events;
        }
    }
}