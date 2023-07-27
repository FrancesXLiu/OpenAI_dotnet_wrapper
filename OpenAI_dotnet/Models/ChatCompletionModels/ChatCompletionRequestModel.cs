﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OpenAI_dotnet;

public class ChatCompletionRequestModel
{
    /// <summary>
    /// ID of the model to use. See the model endpoint compatibility table for details on which models work with the Chat API.
    /// https://platform.openai.com/docs/models/model-endpoint-compatibility
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("model")]
    public string Model { get; set; }

    /// <summary>
    /// The messages to generate chat completions for, in the chat format.
    /// https://platform.openai.com/docs/guides/chat/introduction
    /// </summary>
    [JsonPropertyName("messages")]
    public List<ChatMessageModel> Messages { get; set; }

    /// <summary>
    /// What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.
    /// We generally recommend altering this or top_p but not both.
    /// </summary>
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    /// <summary>
    /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.
    /// We generally recommend altering this or temperature but not both.
    /// </summary>
    [JsonPropertyName("top_p")]
    public double? TopP { get; set; }

    /// <summary>
    /// How many chat completion choices to generate for each input message.
    /// </summary>
    [JsonPropertyName("n")]
    public int? N { get; set; }

    /// <summary>
    /// If set, partial message deltas will be sent, like in ChatGPT. Tokens will be sent as data-only server-sent events as they become available, with the stream terminated by a data: [DONE] message.
    /// </summary>
    [JsonPropertyName("stream")]
    public bool? Stream { get; set; }

    /// <summary>
    /// Up to 4 sequences where the API will stop generating further tokens.
    /// </summary>
    [JsonIgnore]
    public string? Stop { get; set; }

    /// <summary>
    /// Up to 4 sequences where the API will stop generating further tokens.
    /// </summary>
    [JsonIgnore]
    public IList<string>? StopAsList { get; set; }

    [JsonPropertyName("stop")]
    public IList<string>? StopCalculated
    {
        get
        {
            if (Stop != null && StopAsList != null)
            {
                throw new ValidationException("Stop and StopAsList can not be assigned at the same time. One of them is should be null.");
            }

            if (Stop != null)
            {
                return new List<string> { Stop };
            }

            return StopAsList;
        }
    }

    /// <summary>
    /// The maximum number of tokens to generate in the chat completion.
    /// The total length of input tokens and generated tokens is limited by the model's context length.
    /// </summary>
    [JsonPropertyName("max_tokens")]
    public int? MaxTokens { get; set; }

    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the text so far, increasing the model's likelihood to talk about new topics.
    /// </summary>
    [JsonPropertyName("presence_penalty")]
    public double? PresencePenalty { get; set; }

    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in the text so far, decreasing the model's likelihood to repeat the same line verbatim.
    /// </summary>
    [JsonPropertyName("frequency_penalty")]
    public double? FrequencyPenalty { get; set; }

    /// <summary>
    /// Modify the likelihood of specified tokens appearing in the completion.
    /// Accepts a json object that maps tokens (specified by their token ID in the tokenizer) to an associated bias value from -100 to 100. Mathematically, the bias is added to the logits generated by the model prior to sampling. The exact effect will vary per model, but values between -1 and 1 should decrease or increase likelihood of selection; values like -100 or 100 should result in a ban or exclusive selection of the relevant token.
    /// </summary>
    [JsonPropertyName("logit_bias")]
    public object? LogitBias { get; set; }

    /// <summary>
    /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; set; }



    public override string ToString()
    {
        JsonSerializerOptions options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        return JsonSerializer.Serialize(this, options);
    }
}

public class ChatMessageModel
{
    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }
}

