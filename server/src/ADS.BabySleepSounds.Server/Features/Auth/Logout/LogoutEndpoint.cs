﻿using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using ADS.BabySleepSounds.Server.Contracts.Shared.Data;
using ADS.BabySleepSounds.Server.Infrastructure.Persistence;
using ADS.BabySleepSounds.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ADS.BabySleepSounds.Server.Features.Auth.Logout;

public static class LogoutEndpoint
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
            Username = request.Username,
            Password = request.Password
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
                            Name = request.Username,
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
            Token = jwtToken
        });
    }
}
