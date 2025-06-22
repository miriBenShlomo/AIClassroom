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
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AIClassroomDbContext _context;

        public SubCategoryRepository(AIClassroomDbContext context)
        {
            _context = context;
        }

        public async Task AddSubCategoryAsync(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<SubCategory?> GetSubCategoryByIdAsync(int id)
        {
            return await _context.SubCategories.FindAsync(id);
        }

        public async Task<List<SubCategory>> GetAllSubCategoriesAsync()
        {
            return await _context.SubCategories.ToListAsync();
        }

        public async Task UpdateSubCategoryAsync(SubCategory subCategory)
        {
            _context.SubCategories.Update(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubCategoryAsync(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory != null)
            {
                _context.SubCategories.Remove(subCategory);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<SubCategory>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.SubCategories
                                 .Where(sc => sc.CategoryId == categoryId)
                                 .ToListAsync();
        }
    }
}

