using EventService.DTOs.Queries;
using EventService.Models;
using MediatR;

namespace EventService.Application.Queries
{
    public record GetFilteredEventsQuery (
        string City,
        int MinCapacity,
        decimal MinPrice,
        decimal MaxPrice,
        int PageNumber,
        int PageSize ) : IRequest<IEnumerable<Event>>;
}