namespace ADS.BabySleepSounds.Server.Configuration;

public sealed class CorsSettings
{
    internal const string Key = "Cors";

    public string AllowedOrigins { get; init; } = null!;
}
