using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class ChatCompletionResponseModel : BaseResponseModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("created")]
    public long Created { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("usage")]
    public UsageResponseModel Usage { get; set; }

    [JsonPropertyName("choices")]
    public List<ChatCompletionChoice> Choices { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class ChatCompletionChoice
{
    [JsonPropertyName("message")]
    public ChatMessageModel Message { get; set; }

    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; }

    [JsonPropertyName("index")]
    public int Index { get; set; }
}