using BabySounds.Server.Brokers.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabySounds.Server.Features.Categories;

internal static class CategoriesEndpoint
{
    public static async ValueTask<IResult> GetCategories(
        [FromServices] ApplicationDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var categories = await dbContext.Categories
            .ToListAsync(cancellationToken: cancellationToken);

        return Results.Ok(categories);
    }
}
