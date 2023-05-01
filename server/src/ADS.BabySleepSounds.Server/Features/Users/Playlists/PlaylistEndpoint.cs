using Microsoft.AspNetCore.Mvc;

namespace ADS.BabySleepSounds.Server.Features.Users.Playlists;

public static class PlaylistEndpoint
{
    public static async ValueTask<IResult> GetPlaylists([FromRoute] string playlistId, CancellationToken cancellationToken)
    {
        var playlistsResponse = new PlaylistResponse
        {
            UpdateTime = DateTime.UtcNow
        };

        return Results.Ok(playlistsResponse);
    }
}
