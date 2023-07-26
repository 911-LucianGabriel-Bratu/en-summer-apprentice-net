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

        public async Task<OrdersDTO> GetOrderById(long id)
        {
            Order order = await this._ordersRepository.GetOrderById(id);
            OrdersDTO ordersDTO = _mapper.Map<OrdersDTO>(order);
            return ordersDTO;
        }

        public async Task<List<OrdersDTO>> GetOrders()
        {
            List<Order> orders = await this._ordersRepository.GetOrders();
            List<OrdersDTO> ordersDTOs = orders.Select(o => _mapper.Map<OrdersDTO>(o)).ToList();
            return ordersDTOs;
        }

        public async Task<OrdersUpdateDTO> UpdateOrder(long id, OrdersUpdateDTO ordersUpdateDTO)
        {
            return await this._ordersRepository.UpdateOrder(id, ordersUpdateDTO);
        }

        public async Task<OrdersDTO> RemoveOrder(long id)
        {
            return this._mapper.Map<OrdersDTO>(await _ordersRepository.RemoveOrder(id));
        }
    }
}
