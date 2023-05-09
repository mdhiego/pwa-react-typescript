namespace BabySounds.Server.Features.Search;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapSearchEndpoints(this IEndpointRouteBuilder app)
    {
        var searchGroup = app
            .MapGroup("/search")
            .RequireAuthorization()
            .WithTags(nameof(Search));
        {
            searchGroup
                .MapGet("", SearchEndpoint.Search)
                .Produces(StatusCodes.Status200OK);
        }

        return app;
    }
}
