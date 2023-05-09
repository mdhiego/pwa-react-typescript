namespace BabySounds.Server.Features.Categories;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder app)
    {
        var categoriesGroup = app
            .MapGroup("/categories")
            .RequireAuthorization()
            .WithTags(nameof(Categories));
        {
            categoriesGroup
                .MapGet("", CategoriesEndpoint.GetCategories)
                .Produces(StatusCodes.Status200OK);

            categoriesGroup
                .MapGet("{categoryId}", CategoryEndpoint.GetCategory)
                .Produces(StatusCodes.Status200OK);
        }

        return app;
    }
}
