using OpenAI_dotnet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public partial class OpenAIWrapper
{
    public async Task<ModerationResponseModel> CreateModeration(ModerationRequestModel requestContent)
    {
        return await _httpClient.PostAndReadAsAsync<ModerationResponseModel>("https://api.openai.com/v1/moderations", requestContent);
    }
}
