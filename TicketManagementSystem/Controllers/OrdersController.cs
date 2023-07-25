using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.Models;
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
        public ActionResult<List<Order>> GetAll() {
            return Ok(this._ordersService.GetOrders());
        }

        [HttpGet]
        public ActionResult<Order> GetById(int id)
        {
            return Ok(this._ordersService.GetOrderById(id));
        }
    }
}
