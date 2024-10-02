using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Payroll
{
    public int PayrollId { get; set; }

    public int EmpId { get; set; }

    public int JobId { get; set; }

    public int SalaryId { get; set; }

    public int LeaveId { get; set; }

    public DateOnly Date { get; set; }

    public string? Report { get; set; }

    public int TotalAmount { get; set; }
}
