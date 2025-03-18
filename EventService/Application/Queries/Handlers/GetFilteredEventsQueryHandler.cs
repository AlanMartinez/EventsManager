using AutoMapper;
using EventService.Infrastructure;
using EventService.Infrastructure.Models;
using EventService.Models;
using MediatR;

namespace EventService.Application.Queries.Handlers
{
    public class GetFilteredEventsQueryHandler : IRequestHandler<GetFilteredEventsQuery, IEnumerable<Event>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public GetFilteredEventsQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Event>> Handle(GetFilteredEventsQuery request, CancellationToken cancellationToken)
        {
            var filteredEventsParams = _mapper.Map<EventFilteredParams>(request);
            return await _eventRepository.GetFilteredEventAsync(filteredEventsParams);
        }
    }
}