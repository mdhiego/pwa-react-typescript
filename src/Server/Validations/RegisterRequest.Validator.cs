using BabySounds.Contracts.Requests;
using FluentValidation;

namespace BabySounds.Server.Validations;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(r => r.Username)
            .NotNull();

        RuleFor(r => r.Password)
            .NotNull()
            .MinimumLength(8);
    }
}
