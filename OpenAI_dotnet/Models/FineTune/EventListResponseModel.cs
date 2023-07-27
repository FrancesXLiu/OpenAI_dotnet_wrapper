using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class EventListResponseModel : BaseResponseModel
{
    [JsonPropertyName("data")]
    public List<EventResponseModel> Data { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
