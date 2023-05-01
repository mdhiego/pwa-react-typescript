namespace ADS.BabySleepSounds.Server.Features.Auth.Login;

public sealed record LoginResponse
{
    /// <summary>
    /// A JWT token that can be used to authenticate future requests.
    /// </summary>
    public string Token { get; init; } = null!;
}
