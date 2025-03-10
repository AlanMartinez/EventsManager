namespace EventService.DTOs.Queries
{
    public class EventAttendeesRatingDto
    {
        public string Title { get; set; }
        public DateTime InitDate { get; set; }
        public string City { get; set; }
        public int NumAsistentes { get; set; }
        public double PromedioRating { get; set; }
    }
}