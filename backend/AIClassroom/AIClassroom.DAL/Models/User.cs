﻿using AIClassroom.DAL.Models;
using System;
using System.Collections.Generic;

namespace AIClassroom.DAL.Models;


public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }
}
