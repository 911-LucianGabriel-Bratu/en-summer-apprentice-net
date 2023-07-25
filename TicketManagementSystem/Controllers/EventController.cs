using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System;
using TicketManagementSystem.Models.DTOs;

namespace TicketManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<EventDTO>> GetAll()
        {
            var events = new List<EventDTO>();

            events.Add(new EventDTO
            {
                EventId = 1,
                EventName = "Test",
                EventDescription = "Test",
                EventType = "Test",
                Venue = "Test"
            });

            events.Add(new EventDTO
            {
                EventId = 2,
                EventName = "Test",
                EventDescription = "Test",
                EventType= "Test",
                Venue = "Test"
            });

            return Ok(events);
        }

        [HttpGet]
        public ActionResult<EventDTO> GetById(long id) {
            var eventDTO = new EventDTO
            {
                EventId = 2,
                EventName = "Test",
                EventDescription = "Test",
                EventType = "Test",
                Venue = "Test"
            };
            return Ok(eventDTO);
        }
    }
}
