using BabySounds.Server.Brokers.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabySounds.Server.Features.Categories;

internal static class CategoryEndpoint
{
    public static async ValueTask<IResult> GetCategory(
        [FromServices] ApplicationDbContext dbContext,
        [FromRoute] Guid categoryId,
        CancellationToken cancellationToken
    )
    {
        var category = await dbContext.Categories
            .SingleOrDefaultAsync(category => category.Id == categoryId, cancellationToken);

        return Results.Ok(category);
    }
}
