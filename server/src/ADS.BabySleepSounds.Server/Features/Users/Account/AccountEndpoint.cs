using ADS.BabySleepSounds.Server.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ADS.BabySleepSounds.Server.Features.Users.Account;

public static class AccountEndpoint
{
    public static async ValueTask<IResult> GetAccount(
        [FromServices] ApplicationDbContext dbContext,
        [FromRoute] string username,
        CancellationToken cancellationToken
    )
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Username == username, cancellationToken: cancellationToken);
        if (user is null) return Results.NotFound($"The user with the username {username} was not found.");

        var accountsResponse = new AccountResponse
        {
            UpdateTime = DateTime.UtcNow
        };

        return Results.Ok(accountsResponse);
    }
}
