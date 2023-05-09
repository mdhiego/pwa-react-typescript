using BabySounds.Server.Brokers.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabySounds.Server.Features.Playlists;

internal static class PlaylistEndpoint
{
    public static async ValueTask<IResult> GetPlaylist(
        [FromRoute] Guid playlistId,
        [FromServices] ApplicationDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var playlist = await dbContext.Playlists
            .AsNoTracking()
            .FirstOrDefaultAsync(playlist => playlist.Id == playlistId, cancellationToken);

        return Results.Ok(playlist);
    }
}
