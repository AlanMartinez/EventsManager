using EventService.Models;
using MediatR;

public record GetEventByIdQuery(string Id) : IRequest<Event?>;
