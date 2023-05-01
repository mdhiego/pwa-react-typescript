using FluentValidation;

namespace ADS.BabySleepSounds.Server.Features.Auth.Register;

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
