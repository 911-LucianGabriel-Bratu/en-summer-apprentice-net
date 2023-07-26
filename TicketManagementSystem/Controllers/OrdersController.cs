using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;
using TicketManagementSystem.Services;

namespace TicketManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll() {
            return Ok(await this._ordersService.GetOrders());
        }

        [HttpGet]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            OrdersDTO order =  await this._ordersService.GetOrderById(id);
            if(order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
