using System;
using System.Collections.Generic;

namespace bcg_bot.Models;

public partial class User
{
    public long ChatId { get; set; }

    public TimeOnly? DateReg { get; set; }

    public string? Fio { get; set; }

    public string? University { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? BmstuGroup { get; set; }

    public string? Expirience { get; set; }

    public int? UserType { get; set; }

    public string? ComandLine { get; set; }

    public string? PrevComand { get; set; }

    public int? Comand { get; set; }

    public string? Phone { get; set; }

    public int Code { get; set; }

    public string? Link { get; set; }

    public virtual Comand? ComandNavigation { get; set; }
}
