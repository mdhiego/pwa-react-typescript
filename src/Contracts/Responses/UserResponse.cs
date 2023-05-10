namespace BabySounds.Contracts.Responses;

public sealed record UserResponse
{
    public required string FirstName { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }

    public DateTime UpdateTime { get; set; }
}
