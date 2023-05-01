using System.Security.Claims;

namespace ADS.BabySleepSounds.Server.Infrastructure.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(IEnumerable<Claim> claims, int? expiresInSeconds = null);
}
