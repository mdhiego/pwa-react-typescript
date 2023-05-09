using System.Security.Claims;
using BabySounds.Contracts.Responses;
using BabySounds.Server.Brokers.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabySounds.Server.Features.Users;

internal static class UserEndpoint
{
    public static async ValueTask<IResult> GetCurrentUser(
        HttpContext httpContext,
        [FromServices] ApplicationDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var username = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.UserName == username, cancellationToken: cancellationToken);

        if (user is null) return Results.NotFound($"The user with the username {username} was not found.");

        var accountsResponse = new UserResponse
        {
            FirstName = user.FirstName,
            Email = user.Email,
            Username = user.UserName,
            UpdateTime = DateTime.UtcNow
        };

        return Results.Ok(accountsResponse);
    }
}
