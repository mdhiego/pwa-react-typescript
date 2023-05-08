using System.Text.Json.Serialization;

namespace BabySounds.Contracts.Responses;

public sealed record RegisterResponse
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; }

    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }
}
