using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIClassroom.BL.API;
using AIClassroom.DAL.Interfaces;
using AIClassroom.DAL.Models;
using AIClassroom.BL.ModelsDTO;
using AutoMapper;

namespace AIClassroom.BL.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMapper _mapper;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository, IMapper mapper)
        {
            _subCategoryRepository = subCategoryRepository;
            _mapper = mapper;
        }

        public async Task AddSubCategoryAsync(SubCategoryDto subCategoryDto)
        {
            if (string.IsNullOrWhiteSpace(subCategoryDto.Name))
                throw new ArgumentException("SubCategory name cannot be empty.");

            if (subCategoryDto.CategoryId <= 0)
                throw new ArgumentException("Invalid Category ID.");

            var subCategory = _mapper.Map<SubCategory>(subCategoryDto);
            await _subCategoryRepository.AddSubCategoryAsync(subCategory);
        }

        public async Task<SubCategoryDto?> GetSubCategoryByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SubCategory ID.");

            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(id);
            return subCategory != null ? _mapper.Map<SubCategoryDto>(subCategory) : null;
        }

        public async Task<List<SubCategoryDto>> GetAllSubCategoriesAsync()
        {
            var subCategories = await _subCategoryRepository.GetAllSubCategoriesAsync();
            return _mapper.Map<List<SubCategoryDto>>(subCategories);
        }

        public async Task UpdateSubCategoryAsync(SubCategoryDto subCategoryDto)
        {
            if (subCategoryDto.Id <= 0)
                throw new ArgumentException("Invalid SubCategory ID.");

            if (string.IsNullOrWhiteSpace(subCategoryDto.Name))
                throw new ArgumentException("SubCategory name cannot be empty.");

            if (subCategoryDto.CategoryId <= 0)
                throw new ArgumentException("Invalid Category ID.");

            var subCategory = _mapper.Map<SubCategory>(subCategoryDto);
            await _subCategoryRepository.UpdateSubCategoryAsync(subCategory);
        }

        public async Task DeleteSubCategoryAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SubCategory ID.");

            await _subCategoryRepository.DeleteSubCategoryAsync(id);
        }
        public async Task<List<SubCategoryDto>> GetSubCategoriesByCategoryIdAsync(int categoryId)
        {
            var subCategories = await _subCategoryRepository.GetByCategoryIdAsync(categoryId);
            return _mapper.Map<List<SubCategoryDto>>(subCategories);
        }
    }
}

