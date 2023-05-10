namespace BabySounds.Server.Domain;

public sealed record Playlist
{
    public required Guid Id { get; init; }

    public required string Name { get; set; }
    public string? ImagePath { get; set; }
    public bool IsPublic { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdateTime { get; init; }

    public ICollection<Track> Tracks { get; init; } = new List<Track>();
}
