using Microsoft.Extensions.Logging;
using OpenAI_dotnet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public partial class OpenAIWrapper
{
    /// <summary>
    /// Transcribes audio into the input language.
    /// https://platform.openai.com/docs/api-reference/audio/create
    /// </summary>
    /// <param name="requestContent"></param>
    /// <returns></returns>
    public async Task<AudioTranscriptionResponseModel> CreateAudioTranscription(AudioTranscriptionRequestModel requestContent)
    {
        return await HandleAudio(requestContent, "https://api.openai.com/v1/audio/transcriptions");
    }

    /// <summary>
    /// Translates audio into into English.
    /// https://api.openai.com/v1/audio/translations
    /// </summary>
    /// <param name="requestContent"></param>
    /// <returns></returns>
    public async Task<AudioTranscriptionResponseModel> CreateAudioTranslation(AudioTranscriptionRequestModel requestContent)
    {
        return await HandleAudio(requestContent, "https://api.openai.com/v1/audio/translations");
    }

    private async Task<AudioTranscriptionResponseModel> HandleAudio(AudioTranscriptionRequestModel requestContent, string endpoint)
    {
        if (requestContent == null)
        {
            throw new Exception("Request is null");
        } else if (requestContent.File == null)
        {
            throw new Exception("File does not exist.");
        }

        var multipartContent = new MultipartFormDataContent
                {
                    {new ByteArrayContent(requestContent.File), "file", requestContent.FileName},
                    {new StringContent(requestContent.Model), "model" }
                };

        if (requestContent.Language != null)
        {
            multipartContent.Add(new StringContent(requestContent.Language), "language");
        }

        if (requestContent.Prompt != null)
        {
            multipartContent.Add(new StringContent(requestContent.Prompt), "prompt");
        }

        if (requestContent.ResponseFormat != null)
        {
            multipartContent.Add(new StringContent(requestContent.ResponseFormat), "response_format");
        }

        if (requestContent.Temperature != null)
        {
            multipartContent.Add(new StringContent(requestContent.Temperature.ToString()), "temperature");
        }

        if (StaticValues.AudioStatics.ResponseFormat.Json == requestContent.ResponseFormat ||
        StaticValues.AudioStatics.ResponseFormat.VerboseJson == requestContent.ResponseFormat)
        {
            return await _httpClient.PostFileAndReadAsAsync<AudioTranscriptionResponseModel>(endpoint, multipartContent);
        }

        return new AudioTranscriptionResponseModel
        {
            Text = await _httpClient.PostFileAndReadAsStringAsync(endpoint, multipartContent)
        };

    }
}
