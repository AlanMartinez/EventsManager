using MediatR;

namespace EventService.Application.Commands
{
    public record UpdateEventCommand(Guid Id, string Name, string Date) : IRequest<bool>;
}