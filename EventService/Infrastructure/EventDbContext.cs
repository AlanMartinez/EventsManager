﻿using EventService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventService.Infrastructure
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<EventAttendee> EventAttendees { get; set; }
        public DbSet<EventReview> EventReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventAttendee>()
                .HasKey(ea => new { ea.EventId, ea.AttendeeId });

            modelBuilder.Entity<EventAttendee>()
                .HasOne(ea => ea.Event)
                .WithMany(e => e.EventAttendees)
                .HasForeignKey(ea => ea.EventId);

            modelBuilder.Entity<EventAttendee>()
                .HasOne(ea => ea.Attendee)
                .WithMany(a => a.EventAttendees)
                .HasForeignKey(ea => ea.AttendeeId);

            var guidToStringConverter = new ValueConverter<Guid, string>(
                                            v => v.ToString("D").ToLowerInvariant(),
                                            v => Guid.Parse(v)                       
                                        );

            modelBuilder.Entity<Attendee>()
                .Property(a => a.AttendeeId)
                .HasConversion(guidToStringConverter)
                .HasColumnType("TEXT")
                .UseCollation("NOCASE");


        }
    }
}