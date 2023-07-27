using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.Middleware;
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
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public OrdersController(IOrdersService ordersService, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _ordersService = ordersService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll() {
            _logger.LogInformation("Get Request method GetAll() called");
            return Ok(await this._ordersService.GetOrders());
        }

        [HttpGet]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            _logger.LogInformation("Get Request method GetById() called");
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
            _logger.LogInformation(FormattableString.Invariant($"Delete Request method DeleteOrder called with id: '{id}'"));
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
            _logger.LogInformation(FormattableString.Invariant($"Patch Request method UpdateOrder called with id: '{id}' and OrdersUpdateDTO: orderedAt: {ordersUpdateDTO.OrderedAt}, numberOfTickets: {ordersUpdateDTO.NumberOfTickets}"));
            OrdersUpdateDTO order = await this._ordersService.UpdateOrder(id, ordersUpdateDTO);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
