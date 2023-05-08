using BabySounds.Contracts.Requests;
using FluentValidation;

namespace BabySounds.Server.Validations;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(r => r.UserName)
            .NotNull();

        RuleFor(r => r.Password)
            .NotNull()
            .MinimumLength(8);
    }
}
