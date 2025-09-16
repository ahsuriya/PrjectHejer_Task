using System;
using System.Collections.Generic;

namespace PrjectHejer.DataAccess.Models;

public partial class EntityImage
{
    public Guid Id { get; set; }

    public Guid EntityId { get; set; }

    public Guid EntityTypeId { get; set; }

    public string Base64Image { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual EntityType EntityType { get; set; } = null!;
}
