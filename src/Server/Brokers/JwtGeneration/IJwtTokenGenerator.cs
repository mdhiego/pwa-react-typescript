using System.Security.Claims;

namespace BabySounds.Server.Brokers.JwtGeneration;

internal interface IJwtTokenGenerator
{
    string GenerateToken(IEnumerable<Claim> claims, int? expiresInSeconds = null);
}
