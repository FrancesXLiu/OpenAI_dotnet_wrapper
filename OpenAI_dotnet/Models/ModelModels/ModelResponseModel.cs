using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class ModelResponseModel : BaseResponseModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("owned_by")]
    public string OwnedBy { get; set; }

    [JsonPropertyName("permission")]
    public List<object> Permission { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
