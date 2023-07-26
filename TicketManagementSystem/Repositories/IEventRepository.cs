using TicketManagementSystem.Models;

namespace TicketManagementSystem.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetEvents();
        Task<Event> GetEventById(long id);

        Event AddEvent(Event @event);

        void RemoveEvent(Event @event);

        Event UpdateEvent(Event @event);
    }
}
