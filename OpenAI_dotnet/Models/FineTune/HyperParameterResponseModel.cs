using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class HyperParameterResponseModel
{
    [JsonPropertyName("batch_size")] 
    public int? BatchSize { get; set; }

    [JsonPropertyName("learning_rate_multiplier")]
    public double? LearningRateMultiplier { get; set; }

    [JsonPropertyName("n_epochs")] 
    public int? NEpochs { get; set; }

    [JsonPropertyName("prompt_loss_weight")]
    public double? PromptLossWeight { get; set; }
}
