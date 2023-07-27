using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class ImageCreateRequestModel : ImageRequestBaseModel
{
    /// <summary>
    /// A text description of the desired image(s). The maximum length is 1000 characters.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("prompt")]
    public string Prompt { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
