using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class EmbeddingRequestModel
{
    /// <summary>
    /// ID of the model to use. You can use the List models API to see all of your available models, or see our Model overview for descriptions of them.
    /// https://platform.openai.com/docs/models/overview
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    /// <summary>
    /// Input text to get embeddings for, encoded as a string or array of tokens. To get embeddings for multiple inputs in a single request, pass an array of strings or array of token arrays. Each input must not exceed 8192 tokens in length.
    /// </summary>
    [JsonIgnore]
    public List<string>? InputAsList { get; set; }

    /// <summary>
    /// Input text to get embeddings for, encoded as a string or array of tokens. To get embeddings for multiple inputs in a single request, pass an array of strings or array of token arrays. Each input must not exceed 8192 tokens in length.
    /// </summary>
    [JsonIgnore]
    public string? Input { get; set; }

    [JsonPropertyName("input")]
    public IList<string>? InputCalculated
    {
        get
        {
            if (Input != null && InputAsList != null)
            {
                throw new ValidationException("Input and InputAsList can not be assigned at the same time. One of them is should be null.");
            }

            if (Input != null)
            {
                return new List<string> { Input };
            }

            return InputAsList;
        }
    }

    /// <summary>
    /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
    /// </summary>
    [JsonPropertyName("user")]
    public string? User { get; set; }
}
