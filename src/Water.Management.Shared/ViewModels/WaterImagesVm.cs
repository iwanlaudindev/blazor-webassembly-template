using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class WaterImagesVm
{
    [JsonPropertyName("waterSupplyImageId")]
    public Guid WaterSupplyImageId { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("uri")]
    public string? Uri { get; set; }
}
