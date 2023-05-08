using BabySounds.Contracts.Responses;

namespace BabySounds.Server.Features.Playlists;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapPlaylistsEndpoints(this IEndpointRouteBuilder app)
    {
        var user = app
            .MapGroup("/playlists")
            .RequireAuthorization()
            .WithTags(nameof(Playlists));
        {
            user
                .MapGet("{playlistId}", PlaylistEndpoint.GetPlaylist)
                .Produces<UserResponse>(StatusCodes.Status200OK);
        }

        return app;
    }
}
