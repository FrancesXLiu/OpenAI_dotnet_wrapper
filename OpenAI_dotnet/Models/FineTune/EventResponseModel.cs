using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class EventResponseModel
{
    [JsonPropertyName("object")] 
    public string Object { get; set; }

    [JsonPropertyName("created_at")] 
    public int? CreatedAt { get; set; }

    [JsonPropertyName("level")] 
    public string Level { get; set; }

    [JsonPropertyName("message")] 
    public string Message { get; set; }
}
