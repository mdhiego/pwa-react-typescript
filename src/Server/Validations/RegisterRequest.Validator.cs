using BabySounds.Contracts.Requests;
using FluentValidation;

namespace BabySounds.Server.Validations;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(static r => r.FirstName)
            .NotNull();

        RuleFor(static r => r.Email)
            .EmailAddress();

        RuleFor(static r => r.UserName)
            .NotNull();

        RuleFor(static r => r.Password)
            .NotNull()
            .MinimumLength(8);
    }
}
