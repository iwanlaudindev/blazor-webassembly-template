using System.Text.Json.Serialization;

namespace Shared.Wrapper;

public class Error
{
    [JsonPropertyName("innerMessage")]
    public string? InnerMessage { get; set; }
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    [JsonPropertyName("code")]
    public int Code { get; set; }
}

