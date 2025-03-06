using EventService.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventService.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Init date is required.")]
        public DateTime InitDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(50)]
        public string City { get; set; } = string.Empty;

        [MaxLength(50)]
        public EventStateEnum? State { get; set; } = EventStateEnum.ACTIVE;

        [MaxLength(50)]
        public string? Country { get; set; }

        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Owner is required.")]
        [MaxLength(100)]
        public string Owner { get; set; } = string.Empty;

        public int Capacity { get; set; }
        
        public decimal Price { get; set; }

        public string? Tags { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}