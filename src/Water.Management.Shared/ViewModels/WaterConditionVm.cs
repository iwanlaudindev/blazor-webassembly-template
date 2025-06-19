using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class WaterConditionVm
{
    [JsonPropertyName("waterConditionId")]
    public string? WaterConditionId { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
