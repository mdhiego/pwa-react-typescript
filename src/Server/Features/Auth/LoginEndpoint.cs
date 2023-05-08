using System.Security.Claims;
using System.Text.Json;
using BabySounds.Contracts.Requests;
using BabySounds.Contracts.Responses;
using BabySounds.Contracts.Shared.Data;
using BabySounds.Server.Brokers.JwtGeneration;
using BabySounds.Server.Brokers.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BabySounds.Server.Features.Auth;

internal static class LoginEndpoint
{
    public static async ValueTask<IResult> Login(
        [FromServices] ApplicationDbContext dbContext,
        [FromServices] IJwtTokenGenerator jwtTokenGenerator,
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken
    )
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken: cancellationToken);
        if (user is null || !user.Password.SequenceEqual(request.Password)) return Results.Unauthorized();

        var jwtToken = jwtTokenGenerator.GenerateToken(
            new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, request.UserName),
                new("data", JsonSerializer.Serialize(
                    new TokenData
                    {
                        LoggedUser = new LoggedUser
                        {
                            Identification = request.UserName,
                            UserName = user.UserName,
                            LastAccess = DateTime.UtcNow
                        }
                    }
                )),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
        );

        return Results.Ok(new LoginResponse { AccessToken = jwtToken });
    }
}
