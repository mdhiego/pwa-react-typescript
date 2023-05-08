using System.Text.Json.Serialization;

namespace BabySounds.Contracts.Requests;

public sealed record LoginRequest
{
    /// <summary>
    /// The username of the user.
    /// </summary>
    [JsonPropertyName("username")]
    public required string UserName { get; init; }

    /// <summary>
    /// The password of the user.
    /// </summary>
    [JsonPropertyName("password")]
    public required string Password { get; init; }
}
