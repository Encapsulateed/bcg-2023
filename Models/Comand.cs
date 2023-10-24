using System;
using System.Collections.Generic;

namespace bcg_bot.Models;

public partial class Comand
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? Track { get; set; }

    public long Capitan { get; set; }

    public int? UserCount { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
