using System;
using System.Collections.Generic;

namespace TicketManagementSystem.Models;

public partial class Venue
{
    public long VenueId { get; set; }

    public string? Location { get; set; }

    public string? Type { get; set; }

    public int? Capacity { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
