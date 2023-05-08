namespace BabySounds.Server.Domain;

public sealed record Playlist
{
    public Guid Id { get; init; }

    public string Name { get; init; }
    public string ImageUrl { get; init; }
    public bool IsPublic { get; set; }

    public IEnumerable<Track> Tracks { get; init; }
}
