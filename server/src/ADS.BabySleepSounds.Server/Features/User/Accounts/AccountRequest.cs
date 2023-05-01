namespace ADS.BabySleepSounds.Server.Features.User.Accounts;

public record AccountRequest
{
    /// <summary>
    /// Código do cliente.
    /// </summary>
    /// <example>000000001</example>
    public required string ClientCode { get; init; }

    /// <summary>
    /// Número do documento de identificação oficial do usuário.
    /// </summary>
    /// <example>11111111111</example>
    public required string Cpf { get; init; }
}
