using Microsoft.AspNetCore.Mvc;
using AIClassroom.BL.API;
using AIClassroom.BL.ModelsDTO;

namespace AIClassroom.Controllers
{
    /// <summary>
    /// Controller for managing prompts and related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PromptController : ControllerBase
    {
        private readonly IPromptService _promptService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PromptController"/> class.
        /// </summary>
        /// <param name="promptService">The prompt service business logic.</param>
        public PromptController(IPromptService promptService)
        {
            _promptService = promptService;
        }

        /// <summary>
        /// Get all prompts.
        /// </summary>
        [HttpGet(Name = "GetAllPrompts")]
        public async Task<IActionResult> GetAllPrompts()
        {
            var prompts = await _promptService.GetAllPromptsAsync();
            return Ok(prompts);
        }

        /// <summary>
        /// Get a specific prompt by ID.
        /// </summary>
        /// <param name="id">The ID of the prompt.</param>
        [HttpGet("{id}", Name = "GetPromptById")]
        public async Task<IActionResult> GetPromptById(int id)
        {
            var prompt = await _promptService.GetPromptByIdAsync(id);
            if (prompt == null)
                return NotFound();

            return Ok(prompt);
        }

        /// <summary>
        /// Add a new prompt.
        /// </summary>
        /// <param name="promptDto">The prompt data transfer object.</param>
        [HttpPost(Name = "AddPrompt")]
        public async Task<IActionResult> AddPrompt([FromBody] PromptDto promptDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _promptService.AddPromptAsync(promptDto);
            return CreatedAtAction(nameof(GetPromptById), new { id = promptDto.Id }, promptDto);
        }

        /// <summary>
        /// Update an existing prompt.
        /// </summary>
        /// <param name="id">The ID of the prompt to update.</param>
        /// <param name="promptDto">The updated prompt data transfer object.</param>
        [HttpPut("{id}", Name = "UpdatePrompt")]
        public async Task<IActionResult> UpdatePrompt(int id, [FromBody] PromptDto promptDto)
        {
            if (id != promptDto.Id)
                return BadRequest("Prompt ID mismatch.");

            await _promptService.UpdatePromptAsync(promptDto);
            return NoContent();
        }

        /// <summary>
        /// Delete a prompt by ID.
        /// </summary>
        /// <param name="id">The ID of the prompt to delete.</param>
        [HttpDelete("{id}", Name = "DeletePrompt")]
        public async Task<IActionResult> DeletePrompt(int id)
        {
            await _promptService.DeletePromptAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Generate a lesson for a given prompt.
        /// </summary>
        /// <param name="promptDto">The prompt data transfer object.</param>
        [HttpPost("generate-lesson", Name = "GenerateLesson")]
        public async Task<IActionResult> GenerateLesson([FromBody] PromptDto promptDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lesson = await _promptService.GenerateLessonForPromptAsync(promptDto);
            return Ok(new { Lesson = lesson });

        }

        /// <summary>
        /// Get the learning history for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        [HttpGet("user/{userId}/history", Name = "GetLearningHistory")]
        public async Task<IActionResult> GetLearningHistory(int userId)
        {
            var history = await _promptService.GetLearningHistoryByUserIdAsync(userId);
            return Ok(history);
        }
    }
}
