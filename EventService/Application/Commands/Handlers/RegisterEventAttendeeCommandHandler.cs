using EventService.Exceptions;
using EventService.Infrastructure;
using EventService.Models;
using EventService.Models.Enums;
using MediatR;

namespace EventService.Application.Commands.Handlers
{
    public class RegisterEventAttendeeCommandHandler : IRequestHandler<RegisterEventAttendeeCommand, Unit>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IGenericRepository<Attendee> _attendeeRepository;
        private readonly IGenericRepository<EventAttendee> _eventAttendeeRepository;

        public RegisterEventAttendeeCommandHandler(IEventRepository eventRepository, IGenericRepository<Attendee> attendeeRepository, IGenericRepository<EventAttendee> eventAttendeeRepository)
        {
            _eventRepository = eventRepository;
            _attendeeRepository = attendeeRepository;
            _eventAttendeeRepository = eventAttendeeRepository;
        }

        public async Task<Unit> Handle(RegisterEventAttendeeCommand request, CancellationToken cancellationToken)
        {
            var eventObj = await _eventRepository.GetByIdAsync(request.eventId);
            if (eventObj == null || eventObj.State != EventStateEnum.ACTIVE)
                throw new EventNotFoundException($"The event {request.eventId} is not active.");
            

            var attendee = await _attendeeRepository.GetByIdAsync(request.attendeeId) ?? throw new EventNotActiveException($"The attendee {request.attendeeId} is not valid.");

            var req = new EventAttendee
            {
                Event = eventObj,
                Attendee = attendee
            };

            await _eventAttendeeRepository.AddAsync(req);

            return Unit.Value;
        }
    }
}