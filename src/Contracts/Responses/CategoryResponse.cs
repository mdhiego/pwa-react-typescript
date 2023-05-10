namespace BabySounds.Contracts.Responses;

public sealed record CategoryResponse
{
    public string Name { get; set; }
    public string? Description { get; set; }
}
