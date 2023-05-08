namespace BabySounds.Contracts.Shared.Data;

public sealed record LoggedUser
{
    public string Identification { get; init; }
    public string UserName { get; init; }
    public DateTime LastAccess { get; init; }
}
