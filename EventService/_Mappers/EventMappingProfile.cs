using AutoMapper;
using EventService.Application.Commands;
using EventService.Application.Queries;
using EventService.DTOs.AzureFunctions;
using EventService.DTOs.Queries;
using EventService.Infrastructure.Models;
using EventService.Models;

namespace EventService._Mappers
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<CreateEventCommand, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.State, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<Event, EventCreatedFunctionDto>();
            CreateMap<GetFilteredEventsQuery, EventFilteredParams>();
        }
    }
}