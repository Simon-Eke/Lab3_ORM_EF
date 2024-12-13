using System;
using System.Collections.Generic;

namespace Lab3_ORM_EF.Models;

public partial class CourseGradeStat
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public double? AvgGrade { get; set; }

    public string? MinGrade { get; set; }

    public string? MaxGrade { get; set; }
}
