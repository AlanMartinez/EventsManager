namespace EventService.Infrastructure.Models
{
    public class EventFilteredParams
    {
        public string City { get; set; }
        public int MinCapacity { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}