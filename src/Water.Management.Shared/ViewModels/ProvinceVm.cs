using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class ProvinceVm
{
    [JsonPropertyName("provinceId")]
    public string? ProvinceId { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }
    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }
}
