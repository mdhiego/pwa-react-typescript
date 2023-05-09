namespace BabySounds.Server.Features.Playlists;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapPlaylistsEndpoints(this IEndpointRouteBuilder app)
    {
        var playlistGroup = app
            .MapGroup("/playlists")
            .RequireAuthorization()
            .WithTags(nameof(Playlists));
        {
            playlistGroup
                .MapGet("", PlaylistsEndpoint.GetPlaylists)
                .Produces(StatusCodes.Status200OK);

            playlistGroup
                .MapGet("{playlistId}", PlaylistEndpoint.GetPlaylist)
                .Produces(StatusCodes.Status200OK);
        }

        return app;
    }
}
