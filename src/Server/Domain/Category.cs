namespace BabySounds.Server.Domain;

public sealed record Category
{
    public required Guid Id { get; init; }

    public required string Name { get; set; }
    public string? Description { get; set; }
}
