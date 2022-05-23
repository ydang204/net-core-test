using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NetCoreTest.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(IOptions<AppSettings> appSettingOption, ILogger<EmailSenderService> logger)
        {
            _appSettings = appSettingOption.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var client = new SendGridClient(_appSettings.SendGrid.ApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_appSettings.SendGrid.FromEmail, _appSettings.SendGrid.FromName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            var response = await client.SendEmailAsync(msg);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Email queued successfully");
            }
            else
            {
                _logger.LogError("Failed to send email");
            }
        }

        public async Task SendVeiryAccountEmailAsync(string toEmail, string token)
        {
            var message = @$"<p>Welcome to NetCoreTest App</p>
                            <p>Please verify your account by click this <a href='{string.Format(_appSettings.VerifyAccountUrl, token)}'>link</a></p>";
            var subject = "[NetCoreTest] Account verification";
            await SendEmailAsync(toEmail, subject, message);
        }
    }
}