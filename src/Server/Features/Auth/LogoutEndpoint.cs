namespace BabySounds.Server.Features.Auth;

internal static class LogoutEndpoint
{
    public static ValueTask<IResult> Logout(
        CancellationToken cancellationToken
    )
    {
        return new ValueTask<IResult>(Results.Ok());
    }
}
