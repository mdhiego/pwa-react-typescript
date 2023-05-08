using BabySounds.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BabySounds.Server.Features.Playlists;

internal static class PlaylistEndpoint
{
    public static ValueTask<IResult> GetPlaylist([FromRoute] string playlistId, CancellationToken cancellationToken)
    {
        var playlistsResponse = new PlaylistResponse
        {
            UpdateTime = DateTime.UtcNow
        };

        return ValueTask.FromResult(Results.Ok(playlistsResponse));
    }
}
