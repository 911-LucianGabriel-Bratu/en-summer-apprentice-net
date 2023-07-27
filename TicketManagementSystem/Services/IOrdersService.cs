using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;

namespace TicketManagementSystem.Services
{
    public interface IOrdersService
    {
        Task<List<OrdersDTO>> GetOrders();
        Task<OrdersDTO> GetOrderById(long id);

        Order AddOrder(Order order);

        Task<OrdersDTO> RemoveOrder(long id);

        Task<OrdersUpdateDTO> UpdateOrder(long id, OrdersUpdateDTO ordersUpdateDTO);
    }
}
