namespace BabySounds.Server.Domain;

public sealed record Playlist
{
    public Guid Id { get; init; }

    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsPublic { get; set; } = true;

    public IEnumerable<Track> Tracks { get; init; } = new List<Track>();
}
