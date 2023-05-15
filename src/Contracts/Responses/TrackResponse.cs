namespace BabySounds.Contracts.Responses;

public sealed record TrackResponse
{
    public int Order { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }

    public string? Author { get; set; }

    // public TimeSpan Duration { get; set; }
    public string? AudioPath { get; set; }
    public string? ImagePath { get; set; }
    public CategoryResponse? Category { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; init; }
}
