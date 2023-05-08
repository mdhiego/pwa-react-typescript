using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using BabySounds.Contracts.Requests;
using BabySounds.Contracts.Responses;
using BabySounds.Contracts.Shared.Data;
using BabySounds.Server.Brokers.JwtGeneration;
using BabySounds.Server.Brokers.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BabySounds.Server.Features.Auth;

internal static class LogoutEndpoint
{
    public static async ValueTask<IResult> Logout(
        [FromServices] ApplicationDbContext dbContext,
        [FromServices] IJwtTokenGenerator jwtTokenGenerator,
        [FromBody] LogoutRequest request,
        CancellationToken cancellationToken
    )
    {
        dbContext.Users.Add(new Domain.User
        {
            UserName = request.Username,
            PasswordHash = request.Password
        });

        await dbContext.SaveChangesAsync(cancellationToken);

        var jwtToken = jwtTokenGenerator.GenerateToken(
            new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, request.Username),
                new("data", JsonSerializer.Serialize(
                    new TokenData
                    {
                        LoggedUser = new LoggedUser
                        {
                            Identification = request.Username,
                            UserName = request.Username,
                            LastAccess = DateTime.UtcNow
                        }
                    },
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        NumberHandling = JsonNumberHandling.AllowReadingFromString
                    }
                )),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
        );

        return Results.Ok(new LogoutResponse
        {
            AccessToken = jwtToken
        });
    }
}
