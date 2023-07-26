using TicketManagementSystem.Models;

namespace TicketManagementSystem.Repositories
{
    public interface IEventRepository
    {
        TicketManagementSystemDbContext fetchDBContext();
        List<Event> GetEvents();
        Event GetEventById(long id);

        Event AddEvent(Event @event);

        void RemoveEvent(long id);

        Event UpdateEvent(Event @event);
    }
}
