using MailKit.Security;
using Makitools.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace Makitools.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpoHTML)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(
                _configuration["EmailSettings:SenderName"],
                _configuration["EmailSettings:SenderEmail"]));

            email.To.Add(new MailboxAddress("", destinatario));
            email.Subject = asunto;

            var builder = new BodyBuilder { HtmlBody = cuerpoHTML };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            try
            {
                await smtp.ConnectAsync(
                    _configuration["EmailSettings:SmtpServer"]!,
                    int.Parse(_configuration["EmailSettings:Port"]!),
                    SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(
                    _configuration["EmailSettings:SenderEmail"]!,
                    _configuration["EmailSettings:Password"]!);

                await smtp.SendAsync(email);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
