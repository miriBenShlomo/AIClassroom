using AIClassroom.BL.ModelsDTO;
using AIClassroom.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIClassroom.BL.API
{
    public interface ISubCategoryService
    {
        Task AddSubCategoryAsync(SubCategoryDto subCategoryDto);
        Task DeleteSubCategoryAsync(int id);
        Task<List<SubCategoryDto>> GetAllSubCategoriesAsync();
        Task<SubCategoryDto?> GetSubCategoryByIdAsync(int id);
        Task UpdateSubCategoryAsync(SubCategoryDto subCategoryDto);
        Task<List<SubCategoryDto>> GetSubCategoriesByCategoryIdAsync(int categoryId);
    }
}
