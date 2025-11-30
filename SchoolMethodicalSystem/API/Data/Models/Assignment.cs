using System;
using System.Collections.Generic;

namespace API.Data.Models;

public partial class Assignment
{
    public int Id { get; set; }

    public int TeacherId { get; set; }

    public string Event { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Teacher { get; set; } = null!;
}
