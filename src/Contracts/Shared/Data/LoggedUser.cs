namespace BabySounds.Contracts.Shared.Data;

public sealed record LoggedUser
{
    public required string Identification { get; set; }
    public required string UserName { get; set; }
    public DateTime LastAccess { get; set; }
}
