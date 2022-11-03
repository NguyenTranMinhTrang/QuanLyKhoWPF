using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class Unit
    {
        public Unit()
        {
            Objects = new HashSet<Object>();
        }

        public int Id { get; set; }
        public string? DisplayName { get; set; }

        public virtual ICollection<Object> Objects { get; set; }
    }
}
