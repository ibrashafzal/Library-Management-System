using System;
using System.Collections.Generic;

namespace LMS.Entities;

public partial class Book
{
    public int Id { get; set; }

    public string? Tittle { get; set; }

    public string? Author { get; set; }

    public string? Category { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? AvailableQuantity { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
