using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TicketManagementSystem.Models;

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

        public void RemoveEvent(Event @event)
        {
            dbContext.Remove(@event);
            dbContext.SaveChanges();
        }

        public Event UpdateEvent(Event @event)
        {
            dbContext.Update(@event);
            dbContext.SaveChanges();
            return @event;
        }
    }
}
