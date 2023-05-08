using BabySounds.Contracts.Responses;

namespace BabySounds.Server.Features.Tracks;

internal static class TracksEndpoint
{
    public static ValueTask<IResult> GetTracks(CancellationToken cancellationToken)
    {
        var tracksResponse = new TracksResponse
        {
            UpdateTime = DateTime.UtcNow
        };

        return ValueTask.FromResult(Results.Ok(tracksResponse));
    }
}
