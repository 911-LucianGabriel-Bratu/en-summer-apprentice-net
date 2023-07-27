using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;

namespace TicketManagementSystem.Repositories
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(long id);

        Order AddOrder(Order order);

        Task<Order> RemoveOrder(long id);

        Task<OrdersUpdateDTO> UpdateOrder(long id, OrdersUpdateDTO ordersUpdateDTO);
    }
}
