using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ADS.BabySleepSounds.Server.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ADS.BabySleepSounds.Server.Infrastructure.Services;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtBearerSettings _jwtBearerSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtBearerSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
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
            expires: _dateTimeProvider.UtcNow.AddSeconds(expiresInSeconds ?? _jwtBearerSettings.DefaultExpirationTimeInSeconds),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
