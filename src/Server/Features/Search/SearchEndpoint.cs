using BabySounds.Server.Brokers.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabySounds.Server.Features.Search;

internal static class SearchEndpoint
{
    public static async ValueTask<IResult> Search(
        [FromQuery] string query,
        [FromServices] ApplicationDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var searchResult = await dbContext.Tracks
            .ToListAsync(cancellationToken);

        return Results.Ok(searchResult);
    }
}
