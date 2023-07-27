using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class EmbeddingResponseModel : BaseResponseModel
{
    [JsonPropertyName("model")] 
    public string Model { get; set; }

    [JsonPropertyName("data")] 
    public List<EmbeddingData> Data { get; set; }

    [JsonPropertyName("usage")] 
    public UsageResponseModel Usage { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public record EmbeddingData
{
    [JsonPropertyName("index")] 
    public int? Index { get; set; }

    [JsonPropertyName("embedding")] 
    public List<double> Embedding { get; set; }
}