using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserAccount
{
    public string Login { get; set; } 

    public string Pw { get; set; }

    public string UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserGroup { get; set; }

    public string? HomePage { get; set; }

    public string? Duty { get; set; }

    public string? ClientId { get; set; }

    public string? Branch { get; set; }

    public string? RoleId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? SysDate { get; set; }
}
