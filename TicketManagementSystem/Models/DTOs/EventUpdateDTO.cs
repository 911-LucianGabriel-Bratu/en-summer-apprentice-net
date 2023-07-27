namespace TicketManagementSystem.Models.DTOs
{
    public class EventUpdateDTO
    {
        public string? EventDescription { get; set; }

        public string? EventName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
