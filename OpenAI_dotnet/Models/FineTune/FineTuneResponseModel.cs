using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class FineTuneResponseModel : BaseResponseModel
{
    [JsonPropertyName("events")] 
    public List<EventResponseModel> Events { get; set; }

    [JsonPropertyName("fine_tuned_model")] 
    public string FineTunedModel { get; set; }

    [JsonPropertyName("hyperparams")] 
    public HyperParameterResponseModel HyperParams { get; set; }

    [JsonPropertyName("organization_id")] 
    public string OrganizationId { get; set; }

    [JsonPropertyName("result_files")] 
    public List<FileResponseModel> ResultFiles { get; set; }

    [JsonPropertyName("status")] 
    public string Status { get; set; }

    [JsonPropertyName("validation_files")] 
    public List<FileResponseModel> ValidationFiles { get; set; }

    [JsonPropertyName("training_files")] 
    public List<FileResponseModel> TrainingFiles { get; set; }

    [JsonPropertyName("updated_at")] 
    public int? UpdatedAt { get; set; }

    [JsonPropertyName("created_at")] 
    public int CreatedAt { get; set; }
    [JsonPropertyName("id")] 
    public string Id { get; set; }

    [JsonPropertyName("model")] 
    public string Model { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
