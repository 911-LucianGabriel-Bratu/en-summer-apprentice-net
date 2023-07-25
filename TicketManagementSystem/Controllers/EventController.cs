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
        public ActionResult<List<EventDTO>> GetAll()
        {
            List<EventDTO> events = this._eventService.GetEvents();
            return Ok(events);
        }

        [HttpGet]
        public ActionResult<EventDTO> GetById(long id) {
            var @event = this._eventService.GetEventById(id);
            return Ok(@event);
        }
    }
}
