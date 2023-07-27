using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class BaseResponseModel
{
    [JsonPropertyName("object")] 
    public string? ObjectTypeName { get; set; }

    public bool Successful => Error == null;

    [JsonPropertyName("error")] 
    public Error? Error { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class Error
{
    [JsonPropertyName("message")] 
    public string? Message { get; set; }

    [JsonPropertyName("code")] 
    public string? Code { get; set; }

    [JsonPropertyName("param")] 
    public string? Param { get; set; }

    [JsonPropertyName("type")] 
    public string? Type { get; set; }
}