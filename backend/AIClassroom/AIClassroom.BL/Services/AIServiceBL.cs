using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AIClassroom.BL.API;

namespace AIClassroom.BL.Services
{
    public class AIServiceBL : IAIServiceBL
    {
        private readonly HttpClient _httpClient;

        public AIServiceBL(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateLessonAsync(string promptText, int categoryId, int subCategoryId)
        {
            if (string.IsNullOrWhiteSpace(promptText))
                throw new ArgumentException("Prompt text cannot be empty.");

            var requestBody = new
            {
                model = "text-davinci-003",
                prompt = $"Generate a lesson for the following question: {promptText} in category {categoryId}, subcategory {subCategoryId}.",
                max_tokens = 500
            };

            var response = await _httpClient.PostAsJsonAsync("completions", requestBody);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<dynamic>();
            return result?.choices?[0]?.text?.ToString()?.Trim() ?? "No response from AI.";
        }
    }
}
