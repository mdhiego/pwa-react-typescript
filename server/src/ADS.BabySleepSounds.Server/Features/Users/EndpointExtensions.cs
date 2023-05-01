using ADS.BabySleepSounds.Server.Features.Users.Account;
using ADS.BabySleepSounds.Server.Features.Users.Playlists;

namespace ADS.BabySleepSounds.Server.Features.Users;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        var user = app
            .MapGroup("/users")
            .RequireAuthorization()
            .WithTags(nameof(Users));
        {
            user
                .MapGet("{username}/account", AccountEndpoint.GetAccount)
                .Produces<AccountResponse>(StatusCodes.Status200OK);

            user
                .MapGet("{username}/playlists", PlaylistEndpoint.GetPlaylists)
                .Produces<AccountResponse>(StatusCodes.Status200OK);
        }

        return app;
    }
}
