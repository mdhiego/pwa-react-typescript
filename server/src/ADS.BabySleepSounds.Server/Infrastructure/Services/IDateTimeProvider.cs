namespace ADS.BabySleepSounds.Server.Infrastructure.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
