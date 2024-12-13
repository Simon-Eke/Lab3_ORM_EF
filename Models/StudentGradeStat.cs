using System;
using System.Collections.Generic;

namespace Lab3_ORM_EF.Models;

public partial class StudentGradeStat
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public double? AvgGrade { get; set; }

    public string? MinGrade { get; set; }

    public string? MaxGrade { get; set; }
}
