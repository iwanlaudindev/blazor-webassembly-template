using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class DistrictVm
{
    [JsonPropertyName("districtId")]
    public string? DistrictId { get; set; }
    [JsonPropertyName("cityId")]
    public string? CityId { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
