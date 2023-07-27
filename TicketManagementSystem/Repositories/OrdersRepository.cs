using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;

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

        public async Task<Order> RemoveOrder(long id)
        {
            var order = await this.GetOrderById(id);
            if(order != null)
            {
                _dbContext.Remove(order);
                _dbContext.SaveChanges();
                return order;
            }
            return null;
        }

        public async Task<OrdersUpdateDTO> UpdateOrder(long id, OrdersUpdateDTO ordersUpdateDTO)
        {
            var order = await this.GetOrderById(id);

            if(order != null)
            {
                var pricePerTicket = order.TotalPrice / order.NumberOfTickets;
                order.OrderedAt = ordersUpdateDTO.OrderedAt;
                order.NumberOfTickets = ordersUpdateDTO.NumberOfTickets;
                order.TotalPrice = pricePerTicket * ordersUpdateDTO.NumberOfTickets;
                await _dbContext.SaveChangesAsync();
                return ordersUpdateDTO;
            }
            return null;
        }
    }
}
