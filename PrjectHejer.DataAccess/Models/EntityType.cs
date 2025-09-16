using System;
using System.Collections.Generic;

namespace PrjectHejer.DataAccess.Models;

public partial class EntityType
{
    public Guid EntityTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EntityImage> EntityImages { get; set; } = new List<EntityImage>();
}
