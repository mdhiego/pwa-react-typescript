using System.Security.Claims;

namespace BabySounds.Server.Services.JwtGeneration;

internal interface IJwtTokenGenerator
{
    string GenerateToken(IEnumerable<Claim> claims, int? expiresInSeconds = null);
}
