using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class BaseVm
{
    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }
    [JsonPropertyName("createdBy")]
    public string? CreatedBy { get; set; }
    [JsonPropertyName("createdByName")]
    public string? CreatedByName { get; set; }
    [JsonPropertyName("lastUpdatedAt")]
    public DateTime? LastUpdatedAt { get; set; }
    [JsonPropertyName("lastUpdatedBy")]
    public string? LastUpdatedBy { get; set; }
    [JsonPropertyName("lastUpdatedByName")]
    public string? LastUpdatedByName { get; set; }

}
