using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Qualification
{
    public int QualId { get; set; }

    public int EmpId { get; set; }

    public string Position { get; set; } = null!;

    public string Requirements { get; set; } = null!;

    public DateOnly DateIn { get; set; }
}
