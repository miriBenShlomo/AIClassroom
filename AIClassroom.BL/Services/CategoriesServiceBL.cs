using AIClassroom.DAL.Interfaces;
using AIClassroom.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIClassroom.BL.API;
using AIClassroom.BL.ModelsDTO;
using AutoMapper;

namespace AIClassroom.BL.Services

{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task AddCategoryAsync(CategoryDto categoryDto)
        {
            if (string.IsNullOrWhiteSpace(categoryDto.Name))
                throw new ArgumentException("Category name cannot be empty.");

            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Category ID.");

            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return category != null ? _mapper.Map<CategoryDto>(category) : null;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            if (categoryDto.Id <= 0)
                throw new ArgumentException("Invalid Category ID.");

            if (string.IsNullOrWhiteSpace(categoryDto.Name))
                throw new ArgumentException("Category name cannot be empty.");

            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Category ID.");

            await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
