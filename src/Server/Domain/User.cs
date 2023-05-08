using Microsoft.AspNetCore.Identity;

namespace BabySounds.Server.Domain;

public sealed class User : IdentityUser
{
    public string Password { get; init; }

    public ICollection<Playlist> Playlists { get; init; }
}
