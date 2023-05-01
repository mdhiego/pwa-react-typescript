namespace ADS.BabySleepSounds.Server.Configuration;

public sealed class DbSettings
{
    internal const string Key = "ConnectionStrings";

    public string DefaultConnection { get; init; } = null!;
}
