using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class ModerationResponseModel : BaseResponseModel
{
    [JsonPropertyName("results")] 
    public List<ModerationResult> Results { get; set; }

    [JsonPropertyName("id")] 
    public string Id { get; set; }

    [JsonPropertyName("model")] 
    public string Model { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class ModerationResult
{
    [JsonPropertyName("categories")] 
    public ModerationCategories Categories { get; set; }

    [JsonPropertyName("category_scores")] 
    public ModerationCategoryScores CategoryScores { get; set; }

    [JsonPropertyName("flagged")] 
    public bool Flagged { get; set; }
}

public class ModerationCategories
{
    [JsonPropertyName("hate")] 
    public bool Hate { get; set; }

    [JsonPropertyName("hatethreatening")] 
    public bool HateThreatening { get; set; }

    [JsonPropertyName("selfharm")] 
    public bool SelfHarm { get; set; }

    [JsonPropertyName("sexual")] 
    public bool Sexual { get; set; }

    [JsonPropertyName("sexualminors")] 
    public bool SexualMinors { get; set; }

    [JsonPropertyName("violence")] 
    public bool Violence { get; set; }

    [JsonPropertyName("violencegraphic")] 
    public bool ViolenceGraphic { get; set; }
}

public class ModerationCategoryScores
{
    [JsonPropertyName("hate")] 
    public double Hate { get; set; }

    [JsonPropertyName("hatethreatening")] 
    public double HateThreatening { get; set; }

    [JsonPropertyName("selfharm")] 
    public double SelfHarm { get; set; }

    [JsonPropertyName("sexual")] 
    public double Sexual { get; set; }

    [JsonPropertyName("sexualminors")] 
    public double SexualMinors { get; set; }

    [JsonPropertyName("violence")] 
    public double Violence { get; set; }

    [JsonPropertyName("violencegraphic")] 
    public double ViolenceGraphic { get; set; }
}