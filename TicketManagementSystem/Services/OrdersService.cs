using AutoMapper;
using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;
using TicketManagementSystem.Profiles;
using TicketManagementSystem.Repositories;

namespace TicketManagementSystem.Services
{
    public class OrdersService : IOrdersService
    {
        private IOrdersRepository _ordersRepository;
        private IMapper _mapper;

        public OrdersService(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        public Order AddOrder(Order order)
        {
            return this._ordersRepository.AddOrder(order);
        }

        public OrdersDTO GetOrderById(long id)
        {
            Order order = this._ordersRepository.GetOrderById(id);
            OrdersDTO ordersDTO = _mapper.Map<OrdersDTO>(order);
            return ordersDTO;
        }

        public List<OrdersDTO> GetOrders()
        {
            List<Order> orders = this._ordersRepository.GetOrders();
            List<OrdersDTO> ordersDTOs = orders.Select(o => _mapper.Map<OrdersDTO>(o)).ToList();
            return ordersDTOs;
        }

        public void RemoveOrder(long id)
        {
            throw new NotImplementedException();
        }

        public Order UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
