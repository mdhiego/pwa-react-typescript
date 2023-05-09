namespace BabySounds.Contracts.Responses;

public sealed record PlaylistResponse
{
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsPublic { get; set; } = true;

    public DateTime UpdateTime { get; init; }
}
