using FluentValidation;

namespace ADS.BabySleepSounds.Server.Features.User.Accounts;

public class AccountsRequestValidator : AbstractValidator<AccountRequest>
{
    public AccountsRequestValidator()
    {
        RuleFor(r => r.ClientCode)
            .NotNull();
        RuleFor(r => r.Cpf)
            .NotNull()
            .IsValidCPF().Unless(r => r.Cpf.Length == 14)
            .IsValidCNPJ().Unless(r => r.Cpf.Length == 11);
    }
}
