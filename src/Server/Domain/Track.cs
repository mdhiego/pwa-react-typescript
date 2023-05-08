namespace BabySounds.Server.Domain;

public sealed record Track
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
