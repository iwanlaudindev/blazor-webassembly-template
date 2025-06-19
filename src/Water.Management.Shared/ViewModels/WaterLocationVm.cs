using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class WaterLocationVm
{
    [JsonPropertyName("provinceId")]
    public string? ProvinceId { get; set; }
    [JsonPropertyName("province")]
    public string? Province { get; set; }
    [JsonPropertyName("cityId")]
    public string? CityId { get; set; }
    [JsonPropertyName("city")]
    public string? City { get; set; }
    [JsonPropertyName("districtId")]
    public string? DistrictId { get; set; }
    [JsonPropertyName("district")]
    public string? District { get; set; }
    [JsonPropertyName("subDistrictId")]
    public string? SubDistrictId { get; set; }
    [JsonPropertyName("SubDistrict")]
    public string? SubDistrict { get; set; }
    [JsonPropertyName("detailLocation")]
    public string? DetailLocation { get; set; }
}
