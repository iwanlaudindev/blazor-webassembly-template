using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class DashboardSummaryVm
{
    [JsonPropertyName("userCount")]
    public int? UserCount { get; set; }
    [JsonPropertyName("waterSourceCount")]
    public int? WaterSourceCount { get; set; }
    [JsonPropertyName("geophysicsCount")]
    public int? GeophysicsCount { get; set; }
}
