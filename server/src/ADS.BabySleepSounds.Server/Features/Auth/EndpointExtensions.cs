using ADS.BabySleepSounds.Server.Features.Auth.Login;
using ADS.BabySleepSounds.Server.Features.Auth.Register;
using ADS.BabySleepSounds.Server.Features.Common;

namespace ADS.BabySleepSounds.Server.Features.Auth;

public static class EndpointExtensions
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
                .AddEndpointFilter<ValidationFilter<RegisterRequest>>()
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Register endpoint";
                    return operation;
                });

            auth
                .MapPost("login", LoginEndpoint.Login)
                .AllowAnonymous()
                .Accepts<LoginRequest>("application/json")
                .Produces<LoginResponse>(StatusCodes.Status200OK)
                .ProducesValidationProblem()
                .AddEndpointFilter<ValidationFilter<LoginRequest>>()
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Login endpoint";
                    return operation;
                });
        }

        return app;
    }
}
