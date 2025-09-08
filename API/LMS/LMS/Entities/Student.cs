using System;
using System.Collections.Generic;

namespace LMS.Entities;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Class { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
