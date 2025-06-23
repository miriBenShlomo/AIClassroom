using AIClassroom.BL.ModelsDTO;
using AIClassroom.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIClassroom.BL.API
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(CategoryDto categoryDto);
    }
}
