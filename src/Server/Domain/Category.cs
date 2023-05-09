namespace BabySounds.Server.Domain;

public sealed record Category
{
    public Guid Id { get; init; }

    public string Name { get; set; }
    public string? Description { get; set; }
}
