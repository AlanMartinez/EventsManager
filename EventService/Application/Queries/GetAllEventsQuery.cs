using EventService.Controllers;
using EventService.Models;
using MediatR;

public record GetAllEventsQuery() : IRequest<IEnumerable<Event>>;