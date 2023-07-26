using TicketManagementSystem.Models;

namespace TicketManagementSystem.Repositories
{
    public interface IOrdersRepository
    {
        List<Order> GetOrders();
        Order GetOrderById(long id);

        Order AddOrder(Order order);

        void RemoveOrder(Order order);

        Order UpdateOrder(Order order);
    }
}
