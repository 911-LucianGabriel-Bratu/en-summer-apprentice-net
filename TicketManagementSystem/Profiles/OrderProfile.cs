using AutoMapper;
using TicketManagementSystem.Models.DTOs;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Profiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrdersDTO>()
                .ForMember(
                    dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer.CustomerName)
                )
                .ForMember(
                    dest => dest.TicketCategoryDescription,
                    opt => opt.MapFrom(src => src.TicketCategory.Description)
                )
                .ReverseMap();
        }
    }
}
