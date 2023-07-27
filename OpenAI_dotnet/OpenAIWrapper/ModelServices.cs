using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public partial class OpenAIWrapper
{
    /// <summary>
    /// Lists the currently available models, and provides basic information about each one such as the owner and availability.
    /// https://platform.openai.com/docs/api-reference/models/list
    /// </summary>
    public async Task<ModelListResponseModel> ListModels()
    {
        return (await _httpClient.GetFromJsonAsync<ModelListResponseModel>("https://api.openai.com/v1/models"))!;
    }

    /// <summary>
    /// Retrieves a model instance, providing basic information about the model such as the owner and permissioning.
    /// https://platform.openai.com/docs/api-reference/models/retrieve
    /// </summary>
    /// <param name="model">The ID of the model to use for this request</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ModelResponseModel> RetrieveModel(string model)
    {
        try
        {
            return (await _httpClient.GetFromJsonAsync<ModelResponseModel>($"https://api.openai.com/v1/models/{model}"))!;
        } catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }
}
