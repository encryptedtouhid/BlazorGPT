using OpenAI.Interfaces;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels.ResponseModels;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class OpenAIService
    {
        private readonly IOpenAIService _openAIService;

        public OpenAIService(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        [Obsolete]
        public async Task<string> GetCompletionAsync(string prompt)
        {
            var completionResult = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                        {
                            ChatMessage.FromSystem("You are a helpful assistant."),
                            ChatMessage.FromUser(prompt)
                        },
                Model = Models.ChatGpt3_5Turbo,
                MaxTokens = 50 // optional
            });  

            if (completionResult.Successful)
            {
                return completionResult.Choices.FirstOrDefault()?.Message.Content;
            }
            else
            {
                // Handle the case where the API call was not successful
                // You might want to throw an exception or return an error message
                throw new Exception($"OpenAI request failed: {completionResult.Error?.Message}");
            }
        }

    }
}
