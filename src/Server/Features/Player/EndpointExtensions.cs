namespace BabySounds.Server.Features.Player;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapPlayerEndpoints(this IEndpointRouteBuilder app)
    {
        var playerGroup = app
            .MapGroup("/player")
            .RequireAuthorization()
            .WithTags(nameof(Player));
        {
            playerGroup
                .MapGet("play/{playlistId}", PlayEndpoint.Play)
                .Produces(StatusCodes.Status200OK);

            playerGroup
                .MapGet("pause/{playlistId}", PauseEndpoint.Pause)
                .Produces(StatusCodes.Status200OK);
        }

        return app;
    }
}
