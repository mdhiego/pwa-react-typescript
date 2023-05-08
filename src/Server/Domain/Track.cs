namespace BabySounds.Server.Domain;

public sealed record Track
{
    public Guid Id { get; init; }

    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? AudioUrl { get; set; }
}
