namespace ADS.BabySleepSounds.Server.Domain;

public record User
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }

    public string Username { get; init; }
    public string Password { get; init; }

    public ICollection<Playlist> Playlists { get; init; }
}
