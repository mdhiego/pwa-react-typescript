namespace ADS.BabySleepSounds.Server.Contracts.Shared.Data;

public sealed record LoggedUser
{
    public string Identification { get; init; }
    public string Name { get; init; }
    public DateTime LastAccess { get; init; }
}
