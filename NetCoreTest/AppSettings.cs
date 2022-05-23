namespace NetCoreTest
{
    public class AppSettings
    {
        public SendGridSettings SendGrid { get; set; }

        public string VerifyAccountUrl { get; set; }

        public string VerifyAccountSecret { get; set; }
    }

    public class SendGridSettings
    {
        public string FromEmail { get; set; }

        public string FromName { get; set; }

        public string ApiKey { get; set; }
    }
}