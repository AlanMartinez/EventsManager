using EventService.Controllers;
using EventService.Models;
using MediatR;

public record CreateEventCommand(string Name, string Date) : IRequest<Event>;