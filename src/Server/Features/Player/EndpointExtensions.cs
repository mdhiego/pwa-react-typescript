namespace BabySounds.Server.Features.Player;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapPlayerEndpoints(this IEndpointRouteBuilder app)
    {
        var user = app
            .MapGroup("/player")
            .RequireAuthorization()
            .WithTags(nameof(Player));
        {
        }

        return app;
    }
}
