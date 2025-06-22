using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIClassroom.DAL.Models; // הוספת using מתאים

namespace AIClassroom.DAL.Interfaces
{
    public interface IPromptRepository
    {
        Task AddPromptAsync(Prompt prompt);
        Task DeletePromptAsync(int id);
        Task<List<Prompt>> GetAllPromptsAsync();
        Task<Prompt?> GetPromptByIdAsync(int id);
        Task UpdatePromptAsync(Prompt prompt);
    }
}
