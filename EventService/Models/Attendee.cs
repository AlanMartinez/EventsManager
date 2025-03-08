using System.ComponentModel.DataAnnotations;

namespace EventService.Models
{
    public class Attendee
    {
        [Key]
        public Guid AttendeeId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(150)]
        public string Email { get; set; }

        public ICollection<EventAttendee> EventAttendees { get; set; } = new List<EventAttendee>();
    }
}