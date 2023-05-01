using System.Security.Claims;
using System.Text.Json;
using ADS.BabySleepSounds.Server.Contracts.Shared.Data;
using ADS.BabySleepSounds.Server.Infrastructure.Persistence;
using ADS.BabySleepSounds.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ADS.BabySleepSounds.Server.Features.Auth.Login;

public static class LoginEndpoint
{
    public static async ValueTask<IResult> Login(
        [FromServices] ApplicationDbContext dbContext,
        [FromServices] IJwtTokenGenerator jwtTokenGenerator,
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken
    )
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken: cancellationToken);
        if (user is null || !user.Password.Equals(request.Password)) return Results.BadRequest("Invalid username or password");

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
                            Name = user.Name,
                            LastAccess = DateTime.UtcNow
                        }
                    }
                )),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
        );

        return Results.Ok(new LoginResponse
        {
            Token = jwtToken
        });
    }
}
