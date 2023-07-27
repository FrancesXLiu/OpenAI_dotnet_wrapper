using Microsoft.Extensions.Logging;
using OpenAI_dotnet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public partial class OpenAIWrapper
{
    /// <summary>
    /// Given a chat conversation, the model will return a chat completion response.
    /// Creates a completion for the chat message
    /// https://platform.openai.com/docs/api-reference/chat/create
    /// </summary>
    /// <param name="requestContent"></param>
    public async Task<ChatCompletionResponseModel> CreateChatCompletion(ChatCompletionRequestModel requestContent)
    {
        var response = await _httpClient.PostAndReadAsAsync<ChatCompletionResponseModel>("https://api.openai.com/v1/chat/completions", requestContent);
        return response;
    }
}
