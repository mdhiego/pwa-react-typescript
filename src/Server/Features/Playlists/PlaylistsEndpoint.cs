using BabySounds.Server.Brokers.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabySounds.Server.Features.Playlists;

internal static class PlaylistsEndpoint
{
    public static async ValueTask<IResult> GetPlaylists(
        [FromServices] ApplicationDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var playlists = await dbContext.Playlists
            .ToListAsync(cancellationToken: cancellationToken);

        return Results.Ok(playlists);
    }
}
