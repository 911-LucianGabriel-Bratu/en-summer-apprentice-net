using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;

namespace TicketManagementSystem.Services
{
    public interface IOrdersService
    {
        Task<List<OrdersDTO>> GetOrders();
        Task<OrdersDTO> GetOrderById(long id);

        Order AddOrder(Order order);

        void RemoveOrder(long id);

        Order UpdateOrder(Order order);
    }
}
