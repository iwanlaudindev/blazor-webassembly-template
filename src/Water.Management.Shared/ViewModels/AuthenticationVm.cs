using System.Text.Json.Serialization;

namespace Water.Management.Shared.ViewModels;

public class AuthenticationVm
{
    [JsonPropertyName("userId")]
    public Guid UserId { get; }
    [JsonPropertyName("expiry")]
    public long Expiry { get; init; }
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; init; } = null!;
    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; init; } = null!;
}

