namespace BabySounds.Contracts.Responses;

public sealed record TrackResponse
{
    public int Order { get; init; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
    public string? AudioPath { get; set; }
    public CategoryResponse Category { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdateTime { get; init; }
}
