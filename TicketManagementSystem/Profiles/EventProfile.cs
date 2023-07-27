using AutoMapper;
using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;

namespace TicketManagementSystem.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDTO>()
                .ForMember(
                    dest => dest.EventType,
                    opt => opt.MapFrom(src => src.EventType.EventTypeName)
                )
                .ForMember(
                    dest => dest.Venue,
                    opt => opt.MapFrom(src => src.Venue.Location)
                )
                .ReverseMap();
        }
    }
}
