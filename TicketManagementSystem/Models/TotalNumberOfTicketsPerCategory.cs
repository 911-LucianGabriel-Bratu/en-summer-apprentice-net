using System;
using System.Collections.Generic;

namespace TicketManagementSystem.Models;

public partial class TotalNumberOfTicketsPerCategory
{
    public int? TicketCategoryId { get; set; }

    public int? TotalNrOfTickets { get; set; }

    public decimal? FinalTotalPrice { get; set; }
}
