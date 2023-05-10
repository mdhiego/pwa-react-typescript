using System.Text.Json.Serialization;

namespace BabySounds.Contracts.Responses;

public sealed record LoginResponse
{
    [JsonPropertyName("token_type")]
    public required string TokenType { get; init; }

    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }
}
