using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class ChoiceResponseModel
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("index")]
    public int? Index { get; set; }

    [JsonPropertyName("logprobs")]
    public LogProbsResponseModel LogProbs { get; set; }

    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; }
}
