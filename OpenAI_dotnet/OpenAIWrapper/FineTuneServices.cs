using OpenAI_dotnet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenAI_dotnet;

public partial class OpenAIWrapper
{
    /// <summary>
    /// Creates a job that fine-tunes a specified model from a given dataset. 
    /// Response includes details of the enqueued job including job status and the name of the fine-tuned models once complete.
    /// https://platform.openai.com/docs/api-reference/fine-tunes/create
    /// </summary>
    /// <param name="requestContent"></param>
    /// <returns></returns>
    public async Task<FineTuneResponseModel> CreateFineTune(FineTuneRequestModel requestContent)
    {
        return await _httpClient.PostAndReadAsAsync<FineTuneResponseModel>("https://api.openai.com/v1/fine-tunes", requestContent);
    }

    /// <summary>
    /// List your organization's fine-tuning jobs
    /// https://platform.openai.com/docs/api-reference/fine-tunes/list
    /// </summary>
    /// <returns></returns>
    public async Task<FineTuneListResponseModel> ListFineTunes()
    {
        return (await _httpClient.GetFromJsonAsync<FineTuneListResponseModel>("https://api.openai.com/v1/fine-tunes"))!;
    }

    /// <summary>
    /// Gets info about the fine-tune job.
    /// https://platform.openai.com/docs/api-reference/fine-tunes/retrieve
    /// </summary>
    /// <param name="fineTuneId"></param>
    /// <returns></returns>
    public async Task<FineTuneResponseModel> RetrieveFineTune(string fineTuneId)
    {
        return (await _httpClient.GetFromJsonAsync<FineTuneResponseModel>($"https://api.openai.com/v1/fine-tunes/{fineTuneId}"))!;
    }

    /// <summary>
    /// Immediately cancel a fine-tune job.
    /// https://platform.openai.com/docs/api-reference/fine-tunes/cancel
    /// </summary>
    /// <param name="fineTuneId"></param>
    /// <returns></returns>
    public async Task<FineTuneResponseModel> CancelFineTune(string fineTuneId)
    {
        return await _httpClient.PostAndReadAsAsync<FineTuneResponseModel>($"https://api.openai.com/v1/fine-tunes/{fineTuneId}/cancel", fineTuneId);
    }

    /// <summary>
    /// Get fine-grained status updates for a fine-tune job.
    /// https://platform.openai.com/docs/api-reference/fine-tunes/events
    /// </summary>
    /// <param name="fineTuneId"></param>
    /// <returns></returns>
    public async Task<EventListResponseModel> ListFineTuneEvents(string fineTuneId)
    {
        return (await _httpClient.GetFromJsonAsync<EventListResponseModel>($"https://api.openai.com/v1/fine-tunes/{fineTuneId}/events"))!;
    }

    /// <summary>
    /// Delete a fine-tuned model. You must have the Owner role in your organization.
    /// https://platform.openai.com/docs/api-reference/fine-tunes/delete-model
    /// </summary>
    /// <param name="finetuneId"></param>
    /// <returns></returns>
    public async Task<DeleteResponseModel> DeleteFineTune(string finetuneId)
    {
        return await _httpClient.DeleteAndReadAsAsync<DeleteResponseModel>($"https://api.openai.com/v1/models/{finetuneId}");
    }
}
