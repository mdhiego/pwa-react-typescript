using BabySounds.Contracts.Requests;
using FluentValidation;

namespace BabySounds.Server.Validations;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(static r => r.UserName)
            .NotNull();

        RuleFor(static r => r.Password)
            .NotNull()
            .MinimumLength(8);
    }
}
