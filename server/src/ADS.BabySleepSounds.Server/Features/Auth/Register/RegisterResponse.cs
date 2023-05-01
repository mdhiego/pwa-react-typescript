namespace ADS.BabySleepSounds.Server.Features.Auth.Register;

public sealed record RegisterResponse
{
    /// <summary>
    /// A JWT token that can be used to authenticate future requests.
    /// </summary>
    public string Token { get; init; } = null!;
}
