using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using TicketManagementSystem.Exceptions;
using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;

namespace TicketManagementSystem.Repositories
{
    public class EventRepository : IEventRepository
    {
        private TicketManagementSystemDbContext dbContext;

        public EventRepository()
        {
            dbContext = new TicketManagementSystemDbContext();
        }

        public Event AddEvent(Event @event)
        {
            dbContext.Add(@event);
            return @event;
        }

        public async Task<Event> GetEventById(long id)
        {
            Event? @event =  await dbContext.Events
                .Include(e => e.EventType)
                .Where(e => e.EventTypeId != null)
                .Include(e => e.Venue)
                .Where(e => e.VenueId != null)
                .FirstOrDefaultAsync(e => e.EventId == id);

            if(@event == null)
            {
                throw new EntityNotFoundException(id, nameof(Event));
            }

            return @event;
        }

        public async Task<List<Event>> GetEvents()
        {
            List<Event> events = await dbContext.Events
                .Include(e => e.EventType)
                .Where(e => e.EventTypeId != null)
                .Include(e => e.Venue)
                .Where(e => e.VenueId != null)
                .ToListAsync();
            return events;
        }

        public async Task<Event> RemoveEvent(long id)
        {
            var @event = await this.GetEventById(id);
            if(@event != null)
            {
                dbContext.Remove(@event);
                dbContext.SaveChanges();
                return @event;
            }
            else
            {
                throw new EntityNotFoundException(id, nameof(Event));
            }
        }

        public async Task<EventUpdateDTO> UpdateEvent(long id, EventUpdateDTO eventUpdateDTO)
        {
            var @event = await this.GetEventById(id);
            if (@event != null)
            {
                @event.EventDescription = eventUpdateDTO.EventDescription;
                @event.EventName = eventUpdateDTO.EventName;
                @event.StartDate = eventUpdateDTO.StartDate;
                @event.EndDate = eventUpdateDTO.EndDate;
                await dbContext.SaveChangesAsync();
                return eventUpdateDTO;
            }
            else
            {
                throw new EntityNotFoundException(id, nameof(Event));
            }
        }
    }
}
