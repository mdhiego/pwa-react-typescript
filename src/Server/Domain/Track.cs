namespace BabySounds.Server.Domain;

public sealed record Track
{
    public required Guid Id { get; init; }

    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
    public string? AudioPath { get; set; }
    public Category? Category { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateTime { get; init; } = null;
}
