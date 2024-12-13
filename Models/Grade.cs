using System;
using System.Collections.Generic;

namespace Lab3_ORM_EF.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public string FkgradeLetter { get; set; } = null!;

    public DateOnly GradeDate { get; set; }

    public int FkcourseId { get; set; }

    public int FkstudentId { get; set; }

    public int FkteacherId { get; set; }

    public virtual Course Fkcourse { get; set; } = null!;

    public virtual GradesMapping FkgradeLetterNavigation { get; set; } = null!;

    public virtual Student Fkstudent { get; set; } = null!;

    public virtual Employee Fkteacher { get; set; } = null!;
}
