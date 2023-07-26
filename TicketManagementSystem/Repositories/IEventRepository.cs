using TicketManagementSystem.Models;

namespace TicketManagementSystem.Repositories
{
    public interface IEventRepository
    {
        List<Event> GetEvents();
        Event GetEventById(long id);

        Event AddEvent(Event @event);

        void RemoveEvent(Event @event);

        Event UpdateEvent(Event @event);
    }
}
