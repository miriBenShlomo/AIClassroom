using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace AIClassroom.BL.ModelsDTO{
    public class PromptDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string PromptText { get; set; } = null!;
        public string? Response { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}


