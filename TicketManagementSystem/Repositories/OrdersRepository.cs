using TicketManagementSystem.Models;

namespace TicketManagementSystem.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private TicketManagementSystemDbContext _dbContext;

        public OrdersRepository()
        {
            _dbContext = new TicketManagementSystemDbContext();
        }
        public Order AddOrder(Order order)
        {
            this._dbContext.Add(order);
            return order;
        }

        public Order GetOrderById(long id)
        {
            Order order = this._dbContext.Orders.Where(o => o.OrderId == id).FirstOrDefault();
            return order;
        }

        public List<Order> GetOrders()
        {
            return this._dbContext.Orders.ToList();
        }

        public void RemoveOrder(long id)
        {
            this._dbContext.Remove(id);
        }

        public Order UpdateOrder(Order order)
        {
            this._dbContext.Update(order);
            return order;
        }
    }
}
