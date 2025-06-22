using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIClassroom.DAL.Interfaces;
using AIClassroom.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AIClassroom.DAL.Repositories
{
    public class PromptRepository : IPromptRepository
    {
        private readonly AIClassroomDbContext _context;

        public PromptRepository(AIClassroomDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task AddPromptAsync(Prompt prompt)
        {
            _context.Prompts.Add(prompt);
            await _context.SaveChangesAsync();
        }

        // Read
        public async Task<Prompt?> GetPromptByIdAsync(int id)
        {
            return await _context.Prompts.FindAsync(id);
        }

        public async Task<List<Prompt>> GetAllPromptsAsync()
        {
            return await _context.Prompts.ToListAsync();
        }

        // Update
        public async Task UpdatePromptAsync(Prompt prompt)
        {
            _context.Prompts.Update(prompt);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeletePromptAsync(int id)
        {
            var prompt = await _context.Prompts.FindAsync(id);
            if (prompt != null)
            {
                _context.Prompts.Remove(prompt);
                await _context.SaveChangesAsync();
            }
        }
    }
}
