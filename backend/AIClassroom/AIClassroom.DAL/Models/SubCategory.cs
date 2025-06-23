using AIClassroom.DAL.Models;
using System;
using System.Collections.Generic;

namespace AIClassroom.DAL.Models;


public partial class SubCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
}
