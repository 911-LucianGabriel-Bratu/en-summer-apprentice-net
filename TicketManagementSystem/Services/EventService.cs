using AutoMapper;
using Microsoft.Extensions.Logging;
using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;
using TicketManagementSystem.Repositories;

namespace TicketManagementSystem.Services
{
    public class EventService : IEventService
    {
        private IEventRepository _eventRepository;
        private IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public Event AddEvent(Event @event)
        {
            throw new NotImplementedException();
        }

        public async Task<EventDTO> GetEventById(long id)
        {
            var @event = await this._eventRepository.GetEventById(id);
            var eventDTO = _mapper.Map<EventDTO>(@event);
            return eventDTO;
        }

        public async Task<List<EventDTO>> GetEvents()
        {
            var events = await this._eventRepository.GetEvents();
            List <EventDTO> eventsDTO = events.Select(e => _mapper.Map<EventDTO>(e)).ToList();
            return eventsDTO;
        }

        public async Task<EventDTO> RemoveEvent(long id)
        {
            var @event = await this._eventRepository.RemoveEvent(id);
            return _mapper.Map<EventDTO>(@event);
        }

        public async Task<EventUpdateDTO> UpdateEvent(long id, EventUpdateDTO eventUpdateDTO)
        {
            var @event = await this._eventRepository.UpdateEvent(id, eventUpdateDTO);
            return @event;
        }
    }
}
