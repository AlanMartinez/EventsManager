using EventService.Models;
using MediatR;

namespace EventService.Application.Queries
{
    public record GetEventByIdQuery(Guid Id) : IRequest<Event?>;
}