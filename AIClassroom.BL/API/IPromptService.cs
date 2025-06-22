using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIClassroom.DAL.Models; 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIClassroom.BL.ModelsDTO;

namespace AIClassroom.BL.API
{
    public interface IPromptService
    {
        Task AddPromptAsync(PromptDto promptDto);
        Task DeletePromptAsync(int id);
        Task<List<PromptDto>> GetAllPromptsAsync();
        Task<PromptDto?> GetPromptByIdAsync(int id);
        Task UpdatePromptAsync(PromptDto promptDto);
        Task<string> GenerateLessonForPromptAsync(PromptDto promptDto);
        Task<List<PromptDto>> GetLearningHistoryByUserIdAsync(int userId);

    }
}

