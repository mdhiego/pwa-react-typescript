namespace BabySounds.Server.Features.Search;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapSearchEndpoints(this IEndpointRouteBuilder app)
    {
        var user = app
            .MapGroup("/search")
            .RequireAuthorization()
            .WithTags(nameof(Search));
        {
        }

        return app;
    }
}
