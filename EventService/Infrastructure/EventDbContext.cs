using EventService.Models;
using Microsoft.EntityFrameworkCore;

namespace EventService.Infrastructure
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
    }
}