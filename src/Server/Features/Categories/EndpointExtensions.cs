namespace BabySounds.Server.Features.Categories;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder app)
    {
        var user = app
            .MapGroup("/categories")
            .RequireAuthorization()
            .WithTags(nameof(Categories));
        {
        }

        return app;
    }
}
