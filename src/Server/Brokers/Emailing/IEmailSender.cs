namespace BabySounds.Server.Brokers.Emailing;

public interface IEmailSender
{
    Task SendEmailAsync(
        string to,
        string from,
        string subject,
        string body
    );
}
