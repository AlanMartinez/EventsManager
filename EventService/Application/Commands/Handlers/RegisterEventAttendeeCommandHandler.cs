using EventService.Exceptions;
using EventService.Infrastructure;
using EventService.Models;
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
            if (eventObj == null || !eventObj.IsActive)
                throw new EventNotFoundException($"The event {request.eventId} is not active.");
            

            var attendee = await _attendeeRepository.GetByIdAsync(request.attendeeId) ?? throw new AttendeeNotFoundException($"The attendee {request.attendeeId} is not valid.");

            var eventAttendee = await _eventAttendeeRepository.FindAsync(eventObj.Id, attendee.AttendeeId);
            if (eventAttendee != null)
                throw new AttendeeAlreadyRegistered($"The attendee {attendee.AttendeeId} already registered in event {eventObj.Id}.");

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