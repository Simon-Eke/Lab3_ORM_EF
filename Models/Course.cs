using System;
using System.Collections.Generic;

namespace Lab3_ORM_EF.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string CourseSubject { get; set; } = null!;

    public int TeacherId { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Employee Teacher { get; set; } = null!;
}
