﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class FineTuneListResponseModel : BaseResponseModel
{
    [JsonPropertyName("data")] 
    public List<FineTuneResponseModel> Data { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
