using MimeKit;
using System.Net;
using System.Net.Mail;

namespace TrackWallet.Services
{
    public class EmailService
    {
        private readonly string emailSender = "abdullahyahyaalshami2001@gmail.com";
        private readonly string emailPassword = "ihmt dzhq ggtw hohh";

        public async Task SendEmailAsync()
        {

            string fromMail = "abdullahyahyaalshami2001@gmail.com";
            string fromPassword = "ihmt dzhq ggtw hohh";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Welcome";
            message.To.Add(new MailAddress("al7rag50@gmail.com"));
            message.Body = "<html><body>Hi, welcome to my website</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
            Console.WriteLine("Email sent successfully!");
        }
    }

}
