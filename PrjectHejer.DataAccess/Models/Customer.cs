using System;
using System.Collections.Generic;

namespace PrjectHejer.DataAccess.Models;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime CreatedAt { get; set; }
}
