using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class DeleteResponseModel: BaseResponseModel
{
    [JsonPropertyName("deleted")] 
    public bool Deleted { get; set; }
    [JsonPropertyName("id")] 
    public string Id { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
