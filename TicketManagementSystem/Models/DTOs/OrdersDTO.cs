namespace TicketManagementSystem.Models.DTOs
{
    public class OrdersDTO
    {
        public long ? OrderId { get; set; }

        public DateTime? OrderedAt { get; set; }

        public int? NumberOfTickets { get; set; }

        public decimal? TotalPrice { get; set; }
    }
}
