using AIClassroom.BL.API;
using AIClassroom.BL.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AIClassroom.Controllers
{
    /// <summary>
    /// Provides endpoints for retrieving learning categories and their respective sub-categories.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="categoryService">The service for main category operations.</param>
        /// <param name="subCategoryService">The service for sub-category operations.</param>
        public CategoriesController(ICategoryService categoryService, ISubCategoryService subCategoryService)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

        /// <summary>
        /// Gets all available main learning categories.
        /// </summary>
        /// <returns>A list of all main categories.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Gets all sub-categories belonging to a specific main category.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the parent category.</param>
        /// <returns>A list of sub-categories for the specified category, or a 404 Not Found if none exist.</returns>
        [HttpGet("{categoryId}/subcategories")]
        [ProducesResponseType(typeof(IEnumerable<SubCategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SubCategoryDto>>> GetSubCategoriesForCategory(int categoryId)
        {
            var subCategories = await _subCategoryService.GetSubCategoriesByCategoryIdAsync(categoryId);

            if (subCategories == null || !subCategories.Any())
            {
                return NotFound($"No sub-categories found for category with ID {categoryId}.");
            }

            return Ok(subCategories);
        }
    }
}