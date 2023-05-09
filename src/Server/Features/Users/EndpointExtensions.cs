using BabySounds.Contracts.Responses;

namespace BabySounds.Server.Features.Users;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        var userGroup = app
            .MapGroup("/users")
            .RequireAuthorization()
            .WithTags(nameof(Users));
        {
            userGroup
                .MapGet("me", UserEndpoint.GetCurrentUser)
                .Produces<UserResponse>(StatusCodes.Status200OK);
        }

        return app;
    }
}
