namespace BabySounds.Contracts.Responses;

public sealed record UserResponse
{
    public required string FirstName { get; init; }
    public required string Email { get; init; }
    public required string Username { get; init; }

    public DateTime UpdateTime { get; init; }
}
