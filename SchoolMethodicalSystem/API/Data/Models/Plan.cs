using System;
using System.Collections.Generic;

namespace API.Data.Models;

public partial class Plan
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ResponsibleId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Responsible { get; set; } = null!;
}
