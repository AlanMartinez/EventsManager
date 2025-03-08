namespace EventService.Models
{
    public class EventAttendee
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid AttendeeId { get; set; }
        public Attendee Attendee { get; set; }
    }
}