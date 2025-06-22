using AIClassroom.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIClassroom.DAL.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task AddSubCategoryAsync(SubCategory subCategory);
        Task DeleteSubCategoryAsync(int id);
        Task<List<SubCategory>> GetAllSubCategoriesAsync();
        Task<SubCategory?> GetSubCategoryByIdAsync(int id);
        Task UpdateSubCategoryAsync(SubCategory subCategory);
        Task<List<SubCategory>> GetByCategoryIdAsync(int categoryId);
    }
}