using BabySounds.Contracts.Responses;

namespace BabySounds.Server.Features.Tracks;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapTracksEndpoints(this IEndpointRouteBuilder app)
    {
        var songsGroup = app
            .MapGroup("/tracks")
            .RequireAuthorization()
            .WithTags(nameof(Tracks));
        {
            songsGroup
                .MapGet("", TracksEndpoint.GetTracks)
                .Produces<IEnumerable<TracksResponse>>(StatusCodes.Status200OK);

            songsGroup
                .MapGet("{trackId}", TrackEndpoint.GetTrack)
                .Produces<TracksResponse>(StatusCodes.Status200OK);
        }

        return app;
    }
}
