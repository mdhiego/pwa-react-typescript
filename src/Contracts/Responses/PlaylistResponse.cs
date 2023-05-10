namespace BabySounds.Contracts.Responses;

public sealed record PlaylistResponse
{
    public string Name { get; set; }
    public string? ImagePath { get; set; }
    public bool IsPublic { get; set; } = true;

    public DateTime UpdateTime { get; init; }

    public IEnumerable<TrackResponse> Tracks { get; init; } = new List<TrackResponse>();
}
