namespace BabySounds.Server.Configuration;

public sealed class JwtBearerSettings
{
    internal const string Key = "Authentication:JwtBearer";

    public string SecretKey { get; set; }
    public int DefaultExpirationTimeInSeconds { get; init; }
    public string Issuer { get; init; }
    public string? Audience { get; init; }
}
