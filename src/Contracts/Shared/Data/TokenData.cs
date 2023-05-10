namespace BabySounds.Contracts.Shared.Data;

public sealed record TokenData
{
    public required LoggedUser LoggedUser { get; set; }
}
