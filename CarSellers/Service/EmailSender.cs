using CarSellers.Interface;
using MailKit.Net.Smtp;
using MimeKit;

namespace CarSellers.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string emailAddress, string subject, string message)
        {
            var fromEmail = _configuration["Smtp:FromEmail"];
            var host = _configuration["Smtp:Host"];
            var port = Int32.Parse(_configuration["Smtp:Port"]);
            var userName = Environment.GetEnvironmentVariable("SMTP_USERNAME");
            var password = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(fromEmail));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };
            var smtpClient = new SmtpClient();
            smtpClient.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTls);
            smtpClient.Authenticate(userName, password);
            await smtpClient.SendAsync(email);
            smtpClient.Disconnect(true);
        }
    }
}
