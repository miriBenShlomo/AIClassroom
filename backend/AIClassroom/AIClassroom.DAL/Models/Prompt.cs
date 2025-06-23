using AIClassroom.DAL.Models;
using System;
using System.Collections.Generic;

namespace AIClassroom.DAL.Models;


public partial class Prompt
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CategoryId { get; set; }

    public int SubCategoryId { get; set; }

    public string Prompt1 { get; set; } = null!;

    public string? Response { get; set; }

    public DateTime? CreatedAt { get; set; }
}
