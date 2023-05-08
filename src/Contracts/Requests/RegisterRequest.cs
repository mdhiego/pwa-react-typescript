using System.Text.Json.Serialization;

namespace BabySounds.Contracts.Requests;

public sealed record RegisterRequest
{
    [JsonPropertyName("username")]
    public required string Username { get; init; }

    [JsonPropertyName("password")]
    public required string Password { get; init; }
}
