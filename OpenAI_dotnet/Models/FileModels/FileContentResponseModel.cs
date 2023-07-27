using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class FileContentResponseModel<T>
{
    public T? Content { get; set; }
    public bool Successful => Error == null;

    [JsonPropertyName("error")]
    public Error? Error { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
