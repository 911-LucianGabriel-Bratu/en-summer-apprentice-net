using Microsoft.EntityFrameworkCore;
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

        public async Task<Order> GetOrderById(long id)
        {
            Order? order = await this._dbContext.Orders
                .Include(o => o.Customer)
                .Where(o => o.CustomerId != null)
                .Include(o => o.TicketCategory)
                .Where(o => o.TicketCategoryId != null)
                .FirstOrDefaultAsync(o => o.OrderId == id);
            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            List<Order> orders = await this._dbContext.Orders
                .Include(o => o.Customer)
                .Where(o => o.CustomerId != null)
                .Include(o => o.TicketCategory)
                .Where(o => o.TicketCategoryId != null)
                .ToListAsync();
            return orders;
        }

        public void RemoveOrder(Order order)
        {
            this._dbContext.Remove(order);
            this._dbContext.SaveChanges();
        }

        public Order UpdateOrder(Order order)
        {
            this._dbContext.Update(order);
            this._dbContext.SaveChanges();
            return order;
        }
    }
}
