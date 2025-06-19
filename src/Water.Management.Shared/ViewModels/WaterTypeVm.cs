using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class WaterTypeVm
{
    [JsonPropertyName("waterSupplyTypeId")]
    public Guid WaterSupplyTypeId { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
