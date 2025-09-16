using System;
using System.Collections.Generic;

namespace PrjectHejer.DataAccess.Models;

public partial class Lead
{
    public Guid LeadId { get; set; }

    public string LeadName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime CreatedAt { get; set; }
}
