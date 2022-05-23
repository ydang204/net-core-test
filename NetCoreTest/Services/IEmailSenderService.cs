namespace NetCoreTest.Services
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);

        Task SendVeiryAccountEmailAsync(string toEmail, string token);
    }
}