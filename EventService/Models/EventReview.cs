using System.ComponentModel.DataAnnotations;

namespace EventService.Models
{
    public class EventReview
    {
        [Key]
        public Guid ReviewId { get; set; }

        [Required]
        public Guid EventId { get; set; }
        public Event Event { get; set; }

        [MaxLength(100)]
        public string ReviewerName { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(500)]
        public string Comments { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }
    }
}