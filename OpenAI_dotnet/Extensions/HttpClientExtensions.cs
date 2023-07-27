﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenAI_dotnet.Extensions;

public static class HttpClientExtensions
{
    public static async Task<TResponse> PostAndReadAsAsync<TResponse>(this HttpClient client, string uri, object requestModel, CancellationToken cancellationToken = default)
    {
        var response = await client.PostAsJsonAsync(uri, requestModel, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        }, cancellationToken);
        
        return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken) ?? throw new InvalidOperationException();
    }

    public static async Task<TResponse> PostFileAndReadAsAsync<TResponse>(this HttpClient client, string uri, HttpContent content, CancellationToken cancellationToken = default)
    {
        var response = await client.PostAsync(uri, content, cancellationToken);
        return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken) ?? throw new InvalidOperationException();
    }

    public static async Task<string> PostFileAndReadAsStringAsync(this HttpClient client, string uri, HttpContent content, CancellationToken cancellationToken = default)
    {
        var response = await client.PostAsync(uri, content, cancellationToken);
        return await response.Content.ReadAsStringAsync(cancellationToken) ?? throw new InvalidOperationException();
    }

    public static async Task<TResponse> DeleteAndReadAsAsync<TResponse>(this HttpClient client, string uri, CancellationToken cancellationToken = default)
    {
        var response = await client.DeleteAsync(uri, cancellationToken);
        return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken) ?? throw new InvalidOperationException();
    }
}
