using MediatR;

namespace EventService.Application.Commands
{
    public record DeleteEventCommand(Guid Id) : IRequest<bool>;
}