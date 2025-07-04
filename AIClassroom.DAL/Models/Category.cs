﻿using AIClassroom.DAL.Models;
using System;
using System.Collections.Generic;

namespace AIClassroom.DAL.Models;


public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
