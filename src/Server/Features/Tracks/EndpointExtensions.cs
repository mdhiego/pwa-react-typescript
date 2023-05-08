using BabySounds.Contracts.Responses;

namespace BabySounds.Server.Features.Tracks;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapTracksEndpoints(this IEndpointRouteBuilder app)
    {
        var songs = app
            .MapGroup("/tracks")
            .RequireAuthorization()
            .WithTags(nameof(Tracks));
        {
            songs
                .MapGet("", TracksEndpoint.GetTracks)
                .Produces<TracksResponse>(StatusCodes.Status200OK);

            songs
                .MapGet("{trackId}", TrackEndpoint.GetTrack)
                .Produces<TracksResponse>(StatusCodes.Status200OK);
        }

        return app;
    }
}
