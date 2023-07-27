﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class CompletionResponseModel : BaseResponseModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("created")]
    public long Created { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("choices")]
    public List<ChoiceResponseModel> Choices { get; set; }

    [JsonPropertyName("usage")]
    public UsageResponseModel Usage { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}