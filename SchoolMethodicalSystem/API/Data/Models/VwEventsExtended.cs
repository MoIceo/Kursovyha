using System;
using System.Collections.Generic;

namespace API.Data.Models;

public partial class VwEventsExtended
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public string Topic { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int ResponsibleId { get; set; }

    public string ResponsibleName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Report { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
