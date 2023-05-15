using BabySounds.Server.Brokers.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabySounds.Server.Features.Player;

internal static class PauseEndpoint
{
    public static async ValueTask<IResult> Pause(
        [FromServices] ApplicationDbContext dbContext,
        [FromRoute] Guid playlistId,
        CancellationToken cancellationToken
    )
    {
        var playlist = await dbContext.Playlists
            .Include(playlist => playlist.Tracks)
            .SingleOrDefaultAsync(playlist => playlist.Id == playlistId, cancellationToken);

        return Results.Ok(playlist);
    }
}
