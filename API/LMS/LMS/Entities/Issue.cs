using System;
using System.Collections.Generic;

namespace LMS.Entities;

public partial class Issue
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int BookId { get; set; }

    public int IssueQuantity { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime? ExpectedReturnDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int? Fine { get; set; }

    public bool IsReturned { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
