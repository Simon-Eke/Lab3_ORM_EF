using System;
using System.Collections.Generic;

namespace Lab3_ORM_EF.Models;

public partial class GradesMapping
{
    public string GradeLetter { get; set; } = null!;

    public double NumericValue { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
