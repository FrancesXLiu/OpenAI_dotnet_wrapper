﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class FileUploadResponseModel : BaseResponseModel
{
    [JsonPropertyName("id")] 
    public string Id { get; set; }


    [JsonPropertyName("bytes")] 
    public int Bytes { get; set; }


    [JsonPropertyName("filename")] 
    public string FileName { get; set; }


    [JsonPropertyName("purpose")] 
    public string Purpose { get; set; }


    [JsonPropertyName("created_at")] 
    public int CreatedAt { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
