using System;
using System.Collections.Generic;

namespace API.Data.Models;

public partial class VwPlansExtended
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ResponsibleId { get; set; }

    public string ResponsibleName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Period { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
