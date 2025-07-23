using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Rota.Core.Interfaces;
using Rota.Core.Utilities;


namespace Rota.Business.Services
{
    public class MailService : IEmailService
    {

        private readonly EmailSettings _settings;


        public MailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }


        public async Task SendPasswordResetMail(string toMail, string resetLink)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_settings.SenderMail));
            email.To.Add(MailboxAddress.Parse(toMail));
            email.Subject = "Password Reset Request";

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<p>Click the link below to reset your password:</p>" +
                       $"<a href='{resetLink}'>{resetLink}</a><br/><br/>" +
                       $"If you did not request this, please ignore this email."
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.SenderMail, _settings.SenderPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}

