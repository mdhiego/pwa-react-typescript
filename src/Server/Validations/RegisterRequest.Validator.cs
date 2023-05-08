using BabySounds.Contracts.Requests;
using FluentValidation;

namespace BabySounds.Server.Validations;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(static r => r.Name)
            .NotNull();

        RuleFor(static r => r.Email)
            .EmailAddress();

        RuleFor(static r => r.Username)
            .NotNull();

        RuleFor(static r => r.Password)
            .NotNull()
            .MinimumLength(8);
    }
}
