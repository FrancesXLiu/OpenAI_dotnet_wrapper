using Microsoft.Extensions.Logging;
using OpenAI_dotnet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public partial class OpenAIWrapper
{
    /// <summary>
    /// Given a prompt, the model will return one or more predicted completions, and can also return the probabilities of alternative tokens at each position.
    /// https://platform.openai.com/docs/api-reference/completions/create
    /// </summary>
    /// <param name="requestContent"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<CompletionResponseModel> CreateCompletion(CompletionRequestModel requestContent)
    {
        var response = await _httpClient.PostAndReadAsAsync<CompletionResponseModel>("https://api.openai.com/v1/completions", requestContent);
        return response;
    }
}