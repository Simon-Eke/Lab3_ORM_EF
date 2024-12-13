using System;
using System.Collections.Generic;

namespace Lab3_ORM_EF.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? SocialSecurityNumber { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Student? Student { get; set; }
}
