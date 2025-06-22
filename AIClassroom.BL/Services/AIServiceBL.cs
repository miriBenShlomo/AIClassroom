using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::AIClassroom.BL.API;
using System.Net.Http;
using System.Net.Http.Json;


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

            // Prepare the request body for OpenAI API
            var requestBody = new
            {
                model = "text-davinci-003",
                prompt = $"Generate a lesson for the following question: {promptText} in category {categoryId}, subcategory {subCategoryId}.",
                max_tokens = 500
            };

            // Send the request to OpenAI API
            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/completions", requestBody);
            response.EnsureSuccessStatusCode();

            // Parse the response
            var result = await response.Content.ReadFromJsonAsync<dynamic>();
            return result?.choices[0]?.text?.ToString() ?? "No response from AI.";
        }
    }
}
