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

        [HttpPatch]
        [Route("{id:long}")]
        public async Task<ActionResult<EventUpdateDTO>> UpdateEvent([FromRoute] long id, EventUpdateDTO eventUpdateDTO)
        {
            EventUpdateDTO eventUpdate = await this._eventService.UpdateEvent(id, eventUpdateDTO);
            if(eventUpdate == null)
            {
                return NotFound();
            }
            return Ok(eventUpdate);
        }
        [HttpDelete]
        [Route("{id:long}")]
        public async Task<ActionResult<EventDTO>> DeleteEvent([FromRoute] long id)
        {
            var eventDTO =  await this._eventService.RemoveEvent(id);
            if( eventDTO == null)
            {
                return NotFound();
            }
            return Ok(eventDTO);
        }
    }
}
