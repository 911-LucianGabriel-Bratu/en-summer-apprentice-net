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

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<ActionResult<OrdersDTO>> DeleteOrder([FromRoute] long id)
        {
            OrdersDTO order = await this._ordersService.RemoveOrder(id);
            if( order == null )
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPatch]
        [Route("{id:long}")]
        public async Task<ActionResult<OrdersUpdateDTO>> UpdateOrder([FromRoute] long id, OrdersUpdateDTO ordersUpdateDTO)
        {
            OrdersUpdateDTO order = await this._ordersService.UpdateOrder(id, ordersUpdateDTO);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
