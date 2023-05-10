using Microsoft.AspNetCore.Identity;

namespace BabySounds.Server.Domain;

public sealed class User : IdentityUser
{
    internal User()
    {
        NormalizedUserName = UserName?.ToLowerInvariant();
        NormalizedEmail = Email?.ToLowerInvariant();
    }

    public required string FirstName { get; set; }
    public string Password { get; set; }

    public ICollection<Playlist> Playlists { get; init; } = new List<Playlist>();
}
