using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIClassroom.BL.API

{
    public interface IAIServiceBL
    {
        Task<string> GenerateLessonAsync(string promptText, int categoryId, int subCategoryId);
    }
}

