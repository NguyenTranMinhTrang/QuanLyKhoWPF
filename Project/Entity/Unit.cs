using System;
using System.Collections.Generic;

namespace Project.Entity;

public partial class Unit
{
    public int Id { get; set; }

    public string? DisplayName { get; set; }

    public virtual ICollection<Object> Objects { get; } = new List<Object>();
}
