using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class CityVm
{
    [JsonPropertyName("cityId")]
    public string? CityId { get; set; }
    [JsonPropertyName("provinceId")]
    public string? ProvinceId { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
