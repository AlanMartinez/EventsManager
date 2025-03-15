using MediatR;

namespace EventService.Application.Commands
{
    public record RegisterEventAttendeeCommand(Guid eventId, Guid attendeeId) : IRequest<Unit>;
}