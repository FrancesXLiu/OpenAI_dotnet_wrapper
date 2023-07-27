using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class ImageCreateResponseModel : BaseResponseModel
{
    [JsonPropertyName("data")] 
    public List<ImageDataResponseModel> Results { get; set; }

    [JsonPropertyName("created")] 
    public int Created { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class ImageDataResponseModel
{
    [JsonPropertyName("url")] 
    public string Url { get; set; }

    [JsonPropertyName("b64_json")] 
    public string B64Json { get; set; }
}