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

        public Event GetEventById(long id)
        {
            return dbContext.Events.Where(v => v.EventId == id).FirstOrDefault();
        }

        public List<Event> GetEvents()
        {
            return dbContext.Events.ToList();
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
