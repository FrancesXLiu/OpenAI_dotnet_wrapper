using Microsoft.Extensions.Logging;
using OpenAI_dotnet;
using OpenAI_dotnet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

/// https://platform.openai.com/docs/api-reference

namespace OpenAI_dotnet;

public partial class OpenAIWrapper : IOpenAIWrapper
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _organizationID;
    private readonly ILogger<OpenAIWrapper> _logger;

    public OpenAIWrapper(HttpClient httpClient, string apiKey, string organizationID)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
        _organizationID = organizationID;
        _logger = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        }).CreateLogger<OpenAIWrapper>();

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
    }

    public string GetVersion()
    {
        return "V0.1";
    }
}
