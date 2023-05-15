using BabySounds.Server.Brokers.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabySounds.Server.Features.Tracks;

internal static class TracksEndpoint
{
    public static async ValueTask<IResult> GetTracks(
        [FromServices] ApplicationDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var tracks = await dbContext.Tracks
            .Include(track => track.Category)
            .ToListAsync(cancellationToken);

        return Results.Ok(tracks);
    }
}
