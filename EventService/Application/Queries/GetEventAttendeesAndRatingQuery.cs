using EventService.DTOs.Queries;
using MediatR;

namespace EventService.Application.Queries
{
    public record GetEventAttendeesAndRatingQuery() : IRequest<IEnumerable<EventAttendeesRatingDto>?>;
}