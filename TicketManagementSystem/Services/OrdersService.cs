using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;
using TicketManagementSystem.Repositories;

namespace TicketManagementSystem.Services
{
    public class OrdersService : IOrdersService
    {
        private IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public Order AddOrder(Order order)
        {
            return this._ordersRepository.AddOrder(order);
        }

        public OrdersDTO GetOrderById(long id)
        {
            Order order = this._ordersRepository.GetOrderById(id);
            return new OrdersDTO()
            {
                OrderId = order.OrderId,
                TicketCategoryDescription = order.TicketCategory?.Description,
                CustomerName = order.Customer?.CustomerName,
                OrderedAt = order.OrderedAt,
                NumberOfTickets = order.NumberOfTickets,
                TotalPrice = order.TotalPrice
            };
        }

        public List<OrdersDTO> GetOrders()
        {
            List<Order> orders = this._ordersRepository.GetOrders();
            List<OrdersDTO> ordersDTOs = orders.Select(o => new OrdersDTO()
            {
                OrderId=o.OrderId,
                TicketCategoryDescription = o.TicketCategory?.Description,
                CustomerName = o.Customer?.CustomerName,
                OrderedAt = o.OrderedAt,
                NumberOfTickets=o.NumberOfTickets,
                TotalPrice = o.TotalPrice
            }).ToList();
            return ordersDTOs;
        }

        public void RemoveOrder(long id)
        {
            this._ordersRepository.RemoveOrder(id);
        }

        public Order UpdateOrder(Order order)
        {
            return this._ordersRepository.UpdateOrder(order);
        }
    }
}
