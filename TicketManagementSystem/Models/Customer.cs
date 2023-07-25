using System;
using System.Collections.Generic;

namespace TicketManagementSystem.Models;

public partial class Customer
{
    public long CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
