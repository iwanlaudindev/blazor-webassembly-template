using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class RoleVm
{
    [JsonPropertyName("roleId")]
    public string? RoleId { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

