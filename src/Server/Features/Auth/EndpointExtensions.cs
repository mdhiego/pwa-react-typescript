using BabySounds.Contracts.Requests;
using BabySounds.Contracts.Responses;
using BabySounds.Server.Validations;

namespace BabySounds.Server.Features.Auth;

internal static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var auth = app
            .MapGroup("/auth")
            .RequireAuthorization()
            .WithTags(nameof(Auth));
        {
            auth
                .MapPost("register", RegisterEndpoint.Register)
                .AllowAnonymous()
                .Accepts<RegisterRequest>("application/json")
                .Produces<RegisterResponse>(StatusCodes.Status200OK)
                .ProducesValidationProblem()
                .AddEndpointFilter<ValidationFilter<RegisterRequest>>();

            auth
                .MapPost("login", LoginEndpoint.Login)
                .AllowAnonymous()
                .Accepts<LoginRequest>("application/json")
                .Produces<LoginResponse>(StatusCodes.Status200OK)
                .ProducesValidationProblem()
                .AddEndpointFilter<ValidationFilter<LoginRequest>>();
        }

        return app;
    }
}
