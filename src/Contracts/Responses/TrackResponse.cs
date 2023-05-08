namespace BabySounds.Contracts.Responses;

public sealed record TrackResponse
{
    public DateTime UpdateTime { get; init; }
    public int Order { get; set; }
    public string Name { get; set; }
    public string Duration { get; set; }
}
