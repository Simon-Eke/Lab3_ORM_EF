using System;
using System.Collections.Generic;

namespace Lab3_ORM_EF.Models;

public partial class WorkRole
{
    public string WorkRole1 { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
