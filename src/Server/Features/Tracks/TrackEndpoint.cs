using BabySounds.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BabySounds.Server.Features.Tracks;

internal static class TrackEndpoint
{
    public static ValueTask<IResult> GetTrack([FromRoute] string trackId, CancellationToken cancellationToken)
    {
        var tracksResponse = new TrackResponse
        {
            UpdateTime = DateTime.UtcNow
        };

        return ValueTask.FromResult(Results.Ok(tracksResponse));
    }
}
