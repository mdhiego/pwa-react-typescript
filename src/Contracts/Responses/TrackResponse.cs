namespace BabySounds.Contracts.Responses;

public sealed record TrackResponse
{
    public DateTime UpdateTime { get; init; }
    public int Order { get; init; }
    public string Name { get; init; }
    public string Duration { get; init; }
}
