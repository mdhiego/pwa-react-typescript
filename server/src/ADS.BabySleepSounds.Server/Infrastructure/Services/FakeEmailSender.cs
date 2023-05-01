namespace ADS.BabySleepSounds.Server.Infrastructure.Services;

public sealed class FakeEmailSender : IEmailSender
{
    public Task SendEmailAsync(string to, string from, string subject, string body)
    {
        return Task.CompletedTask;
    }
}
