using AIClassroom.BL.API;
using AIClassroom.BL.ModelsDTO;
using AIClassroom.DAL.Interfaces;
using AIClassroom.DAL.Models;
using AutoMapper;

namespace AIClassroom.BL.Services
{
    public class PromptServiceBL : IPromptService
    {
        private readonly IPromptRepository _promptRepository;
        private readonly IAIServiceBL _aiService;
        private readonly IMapper _mapper;

        public PromptServiceBL(IPromptRepository promptRepository, IAIServiceBL aiService, IMapper mapper)
        {
            _promptRepository = promptRepository;
            _aiService = aiService;
            _mapper = mapper;
        }

        public async Task AddPromptAsync(PromptDto promptDto)
        {
            if (promptDto == null)
                throw new ArgumentNullException(nameof(promptDto));

            var prompt = _mapper.Map<Prompt>(promptDto);
            await _promptRepository.AddPromptAsync(prompt);
        }

        public async Task DeletePromptAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid prompt ID.");

            await _promptRepository.DeletePromptAsync(id);
        }

        public async Task<List<PromptDto>> GetAllPromptsAsync()
        {
            var prompts = await _promptRepository.GetAllPromptsAsync();
            return _mapper.Map<List<PromptDto>>(prompts);
        }

        public async Task<PromptDto?> GetPromptByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid prompt ID.");

            var prompt = await _promptRepository.GetPromptByIdAsync(id);
            return _mapper.Map<PromptDto?>(prompt);
        }

        public async Task UpdatePromptAsync(PromptDto promptDto)
        {
            if (promptDto == null)
                throw new ArgumentNullException(nameof(promptDto));

            var prompt = _mapper.Map<Prompt>(promptDto);
            await _promptRepository.UpdatePromptAsync(prompt);
        }

        public async Task<string> GenerateLessonForPromptAsync(PromptDto promptDto)
        {
            if (string.IsNullOrWhiteSpace(promptDto.PromptText))
                throw new ArgumentException("Prompt text cannot be empty.");

            if (promptDto.UserId <= 0 || promptDto.CategoryId <= 0 || promptDto.SubCategoryId <= 0)
                throw new ArgumentException("Invalid UserId, CategoryId, or SubCategoryId.");

            // 👇 קריאה לשירות OpenAI דרך AIServiceBL (שכבר מטפל בשגיאות ו־BaseAddress)
            var lesson = await _aiService.GenerateLessonAsync(
                promptDto.PromptText,
                promptDto.CategoryId,
                promptDto.SubCategoryId
            );

            // שמירת הפרומפט עם התגובה שנוצרה
            var prompt = _mapper.Map<Prompt>(promptDto);
            prompt.Response = lesson;
            prompt.CreatedAt = DateTime.UtcNow;

            await _promptRepository.AddPromptAsync(prompt);

            return lesson;
        }

        public async Task<List<PromptDto>> GetLearningHistoryByUserIdAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid UserId.");

            var prompts = await _promptRepository.GetAllPromptsAsync();
            var userPrompts = prompts.Where(p => p.UserId == userId).ToList();

            return _mapper.Map<List<PromptDto>>(userPrompts);
        }
    }
}
