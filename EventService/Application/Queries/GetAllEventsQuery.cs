using EventService.Models;
using MediatR;

namespace EventService.Application.Queries
{
    public record GetAllEventsQuery() : IRequest<IEnumerable<Event>>;
}