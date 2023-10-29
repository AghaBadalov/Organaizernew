using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using MailKit.Security;
using Organaizer.Models;
using Humanizer;

namespace Organaizer.Services
{
    public class EmailService : IEmailService
    {
        public void Send(Message message)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Organaizer","aghabadalov@yandex.com"));

             foreach (var recipient in message.Recipients)
            {
                email.To.Add(MailboxAddress.Parse(recipient));
            }


            email.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message.Body;
            email.Body = bodyBuilder.ToMessageBody();
            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.yandex.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("aghabadalov@yandex.com", "A1g2a3b4");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
