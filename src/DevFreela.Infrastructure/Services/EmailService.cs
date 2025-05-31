using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DevFreela.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ISendGridClient _sendGridClient;
    private readonly string _fromEmail;
    private readonly string _fromName;

    public EmailService(ISendGridClient sendGridClient, IConfiguration configuration)
    {
        _sendGridClient = sendGridClient;
        _fromEmail = configuration["SendGrid:FromEmail"];
        _fromName = configuration["SendGrid:FromName"];
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        var msg = new SendGridMessage
        {
            From = new EmailAddress(_fromEmail, _fromName),
            Subject = subject,
        };

        msg.AddContent(MimeType.Text, body);
        msg.AddTo(new EmailAddress(to));

        await _sendGridClient.SendEmailAsync(msg);
    }
}
