using ADS.BabySleepSounds.Server.Features.Common;
using ADS.BabySleepSounds.Server.Features.User.Accounts;

namespace ADS.BabySleepSounds.Server.Features.User;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var userV1 = app
            .MapGroup("/user")
            .RequireAuthorization()
            .WithTags(nameof(User));
        {
            userV1
                .MapPost("account", AccountsEndpoint.GetAccount)
                .Accepts<AccountRequest>("application/json")
                .Produces<AccountResponse>(StatusCodes.Status200OK)
                .ProducesValidationProblem()
                .AddEndpointFilter<ValidationFilter<AccountRequest>>()
                .ProducesProblem(StatusCodes.Status401Unauthorized)
                .ProducesProblem(StatusCodes.Status403Forbidden)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(0);
        }

        return app;
    }
}
