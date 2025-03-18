using EventService.DTOs.Queries;
using EventService.Infrastructure;
using MediatR;

namespace EventService.Application.Queries.Handlers
{
    public class GetEventAttendeesAndRatingQueryHandler : IRequestHandler<GetEventAttendeesAndRatingQuery, IEnumerable<EventAttendeesRatingDto>?>
    {
        private readonly IEventRepository _repository;

        public GetEventAttendeesAndRatingQueryHandler(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EventAttendeesRatingDto>?> Handle(GetEventAttendeesAndRatingQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetEventAttendeesRatingAsync();
        }
    }
}