namespace ADS.BabySleepSounds.Server.Configuration;

public sealed class JwtBearerSettings
{
    internal const string Key = "Authentication:JwtBearer";

    public string? SecretKey { get; set; }
    public int DefaultExpirationTimeInSeconds { get; init; }
    public string Issuer { get; init; } = null!;
    public string? Audience { get; init; }
}
