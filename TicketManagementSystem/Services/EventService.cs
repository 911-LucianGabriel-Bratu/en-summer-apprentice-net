using Microsoft.Extensions.Logging;
using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;
using TicketManagementSystem.Repositories;

namespace TicketManagementSystem.Services
{
    public class EventService : IEventService
    {
        private IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public Event AddEvent(Event @event)
        {
            throw new NotImplementedException();
        }

        public EventDTO GetEventById(long id)
        {
            var @event = this._eventRepository.GetEventById(id);
            var eventDTO = new EventDTO
            {
                EventId = @event.EventId,
                EventDescription = @event.EventDescription ?? string.Empty,
                EventName = @event.EventName ?? string.Empty,
                EventType = @event.EventType?.EventTypeName ?? string.Empty,
                Venue = @event.Venue?.Location ?? string.Empty
            };
            return eventDTO;
        }

        public List<EventDTO> GetEvents()
        {
            var events = this._eventRepository.GetEvents();
            List<EventDTO> eventsDTO = events.Select(e => new EventDTO()
            {
                EventId = e.EventId,
                EventDescription = e.EventDescription ?? string.Empty,
                EventName = e.EventName ?? string.Empty,
                EventType = e.EventType?.EventTypeName ?? string.Empty,
                Venue = e.Venue?.Location ?? string.Empty
            }).ToList();
            return eventsDTO;
        }

        public void RemoveEvent(long id)
        {
            throw new NotImplementedException();
        }

        public Event UpdateEvent(Event @event)
        {
            throw new NotImplementedException();
        }
    }
}
