using System.Text.Json.Serialization;

namespace ADS.BabySleepSounds.Server.Features.Auth.Logout;

public sealed record LogoutRequest
{
    [JsonPropertyName("username")]
    public required string Username { get; init; }

    [JsonPropertyName("password")]
    public required string Password { get; init; }
}
