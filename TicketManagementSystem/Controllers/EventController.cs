using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System;
using TicketManagementSystem.Models.DTOs;
using TicketManagementSystem.Services;
using TicketManagementSystem.Middleware;

namespace TicketManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IEventService _eventService;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public EventController(IEventService eventService, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this._eventService = eventService;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDTO>>> GetAll()
        {
            _logger.LogInformation("Get Request method GetAll() called");
            List<EventDTO> events = await this._eventService.GetEvents();
            return Ok(events);
        }

        [HttpGet]
        public async Task<ActionResult<EventDTO>> GetById(long id) {
            _logger.LogInformation("Get Request method GetById() called");
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
            _logger.LogInformation(FormattableString.Invariant($"Patch Request method UpdateEvent called with id: '{id}' and EventUpdateDTO: name: {eventUpdateDTO.EventName}, description: {eventUpdateDTO.EventDescription}, startDate: {eventUpdateDTO.StartDate}, endDate: {eventUpdateDTO.EndDate}"));
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
            _logger.LogInformation(FormattableString.Invariant($"Delete Request method DeleteEvent called with id: '{id}'"));
            var eventDTO =  await this._eventService.RemoveEvent(id);
            if( eventDTO == null)
            {
                return NotFound();
            }
            return Ok(eventDTO);
        }
    }
}
