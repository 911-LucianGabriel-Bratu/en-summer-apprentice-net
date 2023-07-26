using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System;
using TicketManagementSystem.Models.DTOs;
using TicketManagementSystem.Services;

namespace TicketManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IEventService _eventService;

        public EventController(IEventService eventService)
        {
            this._eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDTO>>> GetAll()
        {
            List<EventDTO> events = await this._eventService.GetEvents();
            return Ok(events);
        }

        [HttpGet]
        public async Task<ActionResult<EventDTO>> GetById(long id) {
            var @event = await this._eventService.GetEventById(id);
            if(@event == null)
            {
                return NotFound();
            }
            return Ok(@event);
        }
    }
}
