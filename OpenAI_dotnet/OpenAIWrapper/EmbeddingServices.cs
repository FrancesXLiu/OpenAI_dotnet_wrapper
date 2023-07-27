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
    /// Get a vector representation of a given input that can be easily consumed by machine learning models and algorithms.
    /// Creates an embedding vector representing the input text.
    /// https://platform.openai.com/docs/api-reference/embeddings
    /// </summary>
    /// <param name="requestContent"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<EmbeddingResponseModel> CreateEmbedding(EmbeddingRequestModel requestContent)
    {
        var response = await _httpClient.PostAndReadAsAsync<EmbeddingResponseModel>("https://api.openai.com/v1/embeddings", requestContent);
        return response;
    }
}
