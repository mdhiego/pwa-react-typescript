namespace BabySounds.Contracts.Responses;

public sealed record CategoryResponse
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
