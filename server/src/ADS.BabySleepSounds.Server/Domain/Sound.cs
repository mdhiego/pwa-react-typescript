namespace ADS.BabySleepSounds.Server.Domain;

public sealed record Sound
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string ImageUrl { get; init; }
    public string AudioUrl { get; init; }
    public string AudioFile { get; init; }
    public string AudioFileUrl { get; init; }
    public string AudioFileExtension { get; init; }
}
