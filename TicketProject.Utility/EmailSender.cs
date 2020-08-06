using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketProject.Utility;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions emailOptions;

        public EmailSender(IOptions<EmailOptions> options)
        {
            emailOptions = options.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(emailOptions.SendGridKey, subject, htmlMessage, email);
        }
        private Task Execute(string sendGridKEy, string subject, string message, string email)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("talhaggez@gmail.com", "Talha");
            var to = new EmailAddress(email, "End User");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", message);
            return client.SendEmailAsync(msg);
        }
    }
}