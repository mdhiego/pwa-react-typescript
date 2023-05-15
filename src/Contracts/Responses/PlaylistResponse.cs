namespace BabySounds.Contracts.Responses;

public sealed record PlaylistResponse
{
    public required string Name { get; set; }
    public string? ImagePath { get; set; }
    public bool IsPublic { get; set; } = true;


    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; init; }

    public IEnumerable<TrackResponse> Tracks { get; init; } = new List<TrackResponse>();
}
