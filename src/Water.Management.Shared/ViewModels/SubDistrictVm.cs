using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class SubDistrictVm
{
    [JsonPropertyName("subDistrictId")]
    public string? SubDistrictId { get; set; }
    [JsonPropertyName("districtId")]
    public string? DistrictId { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
