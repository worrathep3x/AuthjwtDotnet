using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Salary
{
    public int SalaryId { get; set; }

    public int JobId { get; set; }

    public int Amount { get; set; }
}
