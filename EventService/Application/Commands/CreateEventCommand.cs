using EventService.Models;
using MediatR;

namespace EventService.Application.Commands
{
    public record CreateEventCommand(
    string Title,
    string Description,
    DateTime InitDate,
    DateTime EndDate,
    string City,
    string Country,
    string Category,
    string Owner,
    int Capacity,
    decimal Price,
    string Tags
    ) : IRequest<Event>;
}