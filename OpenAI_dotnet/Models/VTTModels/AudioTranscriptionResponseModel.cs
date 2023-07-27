using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class AudioTranscriptionResponseModel : BaseResponseModel
{
    [JsonPropertyName("text")]
    private string _rawText { get; set; }

    public string Text
    {
        get
        {
            if (!string.IsNullOrEmpty(_rawText))
            {
                var parsedText = JsonSerializer.Deserialize<Dictionary<string, string>>(_rawText);
                if (parsedText != null && parsedText.TryGetValue("text", out string innerText))
                {
                    return innerText;
                }
            }
            return null;
        }
        set
        {
            _rawText = value;
        }
    }

    [JsonPropertyName("task")] 
    public string Task { get; set; }

    [JsonPropertyName("language")] 
    public string Language { get; set; }

    [JsonPropertyName("duration")] 
    public double Duration { get; set; }

    [JsonPropertyName("segments")] 
    public List<Segment> Segments { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class Segment
{
    [JsonPropertyName("id")] 
    public int Id { get; set; }

    [JsonPropertyName("seek")] 
    public int Seek { get; set; }

    [JsonPropertyName("start")] 
    public float Start { get; set; }

    [JsonPropertyName("end")] 
    public float End { get; set; }

    [JsonPropertyName("text")] 
    public string Text { get; set; }

    [JsonPropertyName("tokens")] 
    public List<int> Tokens { get; set; }

    [JsonPropertyName("temperature")] 
    public float Temperature { get; set; }

    [JsonPropertyName("avg_logprob")] 
    public float AvglogProb { get; set; }

    [JsonPropertyName("compression_ratio")]
    public double CompressionRatio { get; set; }

    [JsonPropertyName("no_speech_prob")] 
    public float NoSpeechProb { get; set; }

    [JsonPropertyName("transient")] 
    public bool Transient { get; set; }
}
