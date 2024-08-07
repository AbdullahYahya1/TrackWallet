using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TrackWallet.Services
{
    public class IEmailSender2 : IEmailSender
    {
        private readonly IConfiguration _Configuration;

        public IEmailSender2(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailSettings = _Configuration.GetSection("EmailSettings");
            var mail = "daslforAufd23@outlook.com";
            var pass = "L123456n";

            var client = new SmtpClient("smtp.office365.com", 587)
            {
                Credentials = new NetworkCredential(mail, pass),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(mail),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true, 
            };
            mailMessage.To.Add(email);

            return client.SendMailAsync(mailMessage);
        }
    }
}
