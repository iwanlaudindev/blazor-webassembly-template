using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class UserVm : BaseVm
{
    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }
    [JsonPropertyName("username")]
    public string? Username { get; set; }
    [JsonPropertyName("fullName")]
    public string? FullName { get; set; }
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }
    [JsonPropertyName("roleId")]
    public string? RoleId { get; set; }
    [JsonPropertyName("role")]
    public string? Role { get; set; }
}

