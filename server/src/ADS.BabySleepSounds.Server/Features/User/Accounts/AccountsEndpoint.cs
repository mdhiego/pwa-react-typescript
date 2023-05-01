namespace ADS.BabySleepSounds.Server.Features.User.Accounts;

public static class AccountsEndpoint
{
    public static async ValueTask<IResult> GetAccount(
        AccountRequest request,
        CancellationToken cancellationToken
    )
    {
        var accountsResponse = new AccountResponse
        {
            UpdateTime = DateTime.UtcNow
        };

        return Results.Ok(accountsResponse);
    }
}
