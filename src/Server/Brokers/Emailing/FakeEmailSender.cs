namespace BabySounds.Server.Brokers.Emailing;

public sealed class FakeEmailSender : IEmailSender
{
    public Task SendEmailAsync(
        string to,
        string from,
        string subject,
        string body
    )
    {
        return Task.CompletedTask;
    }
}
