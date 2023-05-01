namespace ADS.BabySleepSounds.Server.Infrastructure.Services;

public interface IEmailSender
{
    Task SendEmailAsync(
        string to,
        string from,
        string subject,
        string body
    );
}
