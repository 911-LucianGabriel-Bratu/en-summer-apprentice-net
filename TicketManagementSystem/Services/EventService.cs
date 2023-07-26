﻿using AutoMapper;
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

        public EventDTO GetEventById(long id)
        {
            var @event = this._eventRepository.GetEventById(id);
            var eventDTO = _mapper.Map<EventDTO>(@event);
            return eventDTO;
        }

        public List<EventDTO> GetEvents()
        {
            var events = this._eventRepository.GetEvents();
            List<EventDTO> eventsDTO = events.Select(e => _mapper.Map<EventDTO>(e)).ToList();
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
