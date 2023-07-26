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

        public TicketManagementSystemDbContext fetchDBContext()
        {
            return dbContext;
        }
        public Event AddEvent(Event @event)
        {
            dbContext.Add(@event);
            return @event;
        }

        public Event GetEventById(long id)
        {
            return dbContext.Events
                .Include(e => e.EventType)
                .Where(e => e.EventTypeId != null)
                .Include(e => e.Venue)
                .Where(e => e.VenueId != null)
                .FirstOrDefault(e => e.EventId == id);
        }

        public List<Event> GetEvents()
        {
            return dbContext.Events
                .Include(e => e.EventType)
                .Where(e => e.EventTypeId != null)
                .Include(e => e.Venue)
                .Where(e => e.VenueId != null)
                .ToList();
        }

        public void RemoveEvent(long id)
        {
            dbContext.Remove(id);
        }

        public Event UpdateEvent(Event @event)
        {
            dbContext.Update(@event);
            return @event;
        }
    }
}
