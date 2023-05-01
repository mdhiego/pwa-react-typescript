using FluentValidation;

namespace ADS.BabySleepSounds.Server.Features.Auth.Login;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(r => r.Username)
            .NotNull();

        RuleFor(r => r.Password)
            .NotNull()
            .MinimumLength(8);
    }
}