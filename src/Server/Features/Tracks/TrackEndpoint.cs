
using BabySounds.Server.Brokers.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace BabySounds.Server.Features.Tracks;

internal static class TrackEndpoint
{
    public static async ValueTask<IResult> GetTrack(
        [FromRoute] string trackId,
        [FromServices] ApplicationDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var track = await dbContext.Tracks.FindAsync(trackId);

        return Results.Ok(track);
    }
}
