using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Employee
{
    public int EmpId { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public int? Gender { get; set; }

    public int? Age { get; set; }

    public int Phone { get; set; }

    public string EmpEmail { get; set; } = null!;

    public string EmpPass { get; set; } = null!;
}
