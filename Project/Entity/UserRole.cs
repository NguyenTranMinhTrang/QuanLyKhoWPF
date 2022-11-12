﻿using System;
using System.Collections.Generic;

namespace Project.Entity;

public partial class UserRole
{
    public int Id { get; set; }

    public string? DisplayName { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
