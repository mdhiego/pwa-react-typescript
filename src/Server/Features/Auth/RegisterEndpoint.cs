﻿using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using BabySounds.Contracts.Requests;
using BabySounds.Contracts.Responses;
using BabySounds.Contracts.Shared.Data;
using BabySounds.Server.Brokers.JwtGeneration;
using BabySounds.Server.Brokers.Persistence;
using BabySounds.Server.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BabySounds.Server.Features.Auth;

internal static class RegisterEndpoint
{
    public static async ValueTask<IResult> Register(
        [FromServices] ApplicationDbContext dbContext,
        [FromServices] IJwtTokenGenerator jwtTokenGenerator,
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken
    )
    {
        dbContext.Users.Add(new User
        {
            FirstName = request.FirstName,
            UserName = request.Username,
            NormalizedUserName = request.Username.Trim().ToLowerInvariant(),
            Email = request.Email,
            NormalizedEmail = request.Email.Trim().ToLowerInvariant(),
            EmailConfirmed = false,
            TwoFactorEnabled = false,
            Password = request.Password,
            Playlists = new List<Playlist>
            {
                new Playlist
                {
                    Name = "Favorites",
                    IsPublic = false,
                    Tracks = new List<Track>()
                }
            }
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

        return Results.Ok(new RegisterResponse
        {
            TokenType = JwtBearerDefaults.AuthenticationScheme,
            AccessToken = jwtToken
        });
    }
}
