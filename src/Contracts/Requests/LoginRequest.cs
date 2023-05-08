namespace BabySounds.Contracts.Requests;

public sealed record LoginRequest
{
    /// <summary>
    /// The username of the user.
    /// </summary>
    public required string UserName { get; init; }

    /// <summary>
    /// The password of the user.
    /// </summary>
    public required string Password { get; init; }
}
