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
    /// Given a prompt and an instruction, the model will return an edited version of the prompt.
    /// Creates a new edit for the provided input, instruction, and parameters.
    /// https://platform.openai.com/docs/api-reference/edits/create
    /// </summary>
    /// <param name="requestContent"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<EditResponseModel> CreateEdit(EditRequestModel requestContent)
    {
        var response = await _httpClient.PostAndReadAsAsync<EditResponseModel>("https://api.openai.com/v1/edits", requestContent);
        return response;
    }
}
