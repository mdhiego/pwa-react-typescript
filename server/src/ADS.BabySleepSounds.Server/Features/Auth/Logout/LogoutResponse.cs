namespace ADS.BabySleepSounds.Server.Features.Auth.Logout;

public sealed record LogoutResponse
{
    /// <summary>
    /// A JWT token that can be used to authenticate future requests.
    /// </summary>
    public string Token { get; init; } = null!;
}
