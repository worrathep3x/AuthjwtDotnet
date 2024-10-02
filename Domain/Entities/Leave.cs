using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Leave
{
    public int LeaveId { get; set; }

    public int EmpId { get; set; }

    public DateOnly Date { get; set; }

    public string Reason { get; set; } = null!;
}
