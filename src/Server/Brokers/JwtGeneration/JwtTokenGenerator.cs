using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BabySounds.Server.Brokers.SystemClock;
using BabySounds.Server.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BabySounds.Server.Brokers.JwtGeneration;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly ISystemClock _systemClock;
    private readonly JwtBearerSettings _jwtBearerSettings;

    public JwtTokenGenerator(ISystemClock systemClock, IOptions<JwtBearerSettings> jwtOptions)
    {
        _systemClock = systemClock;
        _jwtBearerSettings = jwtOptions.Value;
    }

    public string GenerateToken(IEnumerable<Claim>? claims, int? expiresInSeconds = null)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtBearerSettings.SecretKey!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            issuer: _jwtBearerSettings.Issuer,
            audience: _jwtBearerSettings.Audience,
            claims: claims,
            expires: _systemClock.UtcNow.AddSeconds(expiresInSeconds ?? _jwtBearerSettings.DefaultExpirationTimeInSeconds).DateTime,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
