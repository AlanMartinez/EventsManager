using EventService.Controllers;
using EventService.Infrastructure;
using EventService.Models;
using MediatR;

namespace EventService.Application.Queries.Handlers
{
    public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, IEnumerable<Event>>
    {
        private readonly IGenericRepository<Event> _repository;

        public GetAllEventsQueryHandler(IGenericRepository<Event> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
