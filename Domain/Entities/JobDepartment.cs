using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class JobDepartment
{
    public int JobId { get; set; }

    public string JobDept { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? SalaryRange { get; set; }
}
