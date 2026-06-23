using System.Net;
using System.Net.Mail;

namespace ResumeWebApp.Mail
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public SmtpEmailSender(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            string host = configuration["Smtp:Host"];
            string portValue = configuration["Smtp:Port"];
            string email = configuration["Smtp:Email"];
            string password = configuration["Smtp:Password"];
            string displayName = configuration["Smtp:DisplayName"] ?? "Portfolio Admin";

            int port = string.IsNullOrEmpty(portValue) ? 587 : Convert.ToInt32(portValue);

            using var message = new MailMessage();
            message.From = new MailAddress(email, displayName);
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = body;

            using var client = new SmtpClient(host, port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(email, password);

            await client.SendMailAsync(message);
        }
    }
}
