using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;

namespace TicketManagementSystem.Services
{
    public interface IEventService
    {
        Task<List<EventDTO>> GetEvents();
        Task<EventDTO> GetEventById(long id);

        Event AddEvent(Event @event);

        void RemoveEvent(long id);

        Event UpdateEvent(Event @event);
    }
}
