using EventService.Models;
using MediatR;

namespace EventService.Application.Queries
{
    public record GetEventByIdQuery(string Id) : IRequest<Event?>;
}