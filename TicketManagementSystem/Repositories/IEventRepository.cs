using TicketManagementSystem.Models;
using TicketManagementSystem.Models.DTOs;

namespace TicketManagementSystem.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetEvents();
        Task<Event> GetEventById(long id);

        Event AddEvent(Event @event);

        void RemoveEvent(Event @event);

        Task<EventUpdateDTO> UpdateEvent(long id, EventUpdateDTO eventUpdateDTO);
    }
}
