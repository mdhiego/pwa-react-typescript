namespace ADS.BabySleepSounds.Server.Domain;

public sealed record Playlist
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string ImageUrl { get; init; }
    public IEnumerable<Sound> Sounds { get; init; }
}
