using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;

namespace TicketManagementSystem.Services
{
    public interface IEventService
    {
        List<EventDTO> GetEvents();
        EventDTO GetEventById(long id);

        Event AddEvent(Event @event);

        void RemoveEvent(long id);

        Event UpdateEvent(Event @event);
    }
}
