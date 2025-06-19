using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class CoordinateVm
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("type")]
    public string? type { get; set; }
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}
