using BabySounds.Contracts.Requests;
using BabySounds.Contracts.Responses;
using BabySounds.Server.Validations;

namespace BabySounds.Server.Features.Auth;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var authGroup = app
            .MapGroup("/auth")
            .RequireAuthorization()
            .WithTags(nameof(Auth));
        {
            authGroup
                .MapPost("register", RegisterEndpoint.Register)
                .AllowAnonymous()
                .Accepts<RegisterRequest>("application/json")
                .Produces<RegisterResponse>(StatusCodes.Status200OK)
                .ProducesValidationProblem()
                .AddEndpointFilter<ValidationFilter<RegisterRequest>>();

            authGroup
                .MapPost("login", LoginEndpoint.Login)
                .AllowAnonymous()
                .Accepts<LoginRequest>("application/json")
                .Produces<LoginResponse>(StatusCodes.Status200OK)
                .ProducesValidationProblem()
                .AddEndpointFilter<ValidationFilter<LoginRequest>>();

            authGroup
                .MapPost("logout", LogoutEndpoint.Logout)
                .Produces(StatusCodes.Status200OK);
        }

        return app;
    }
}
