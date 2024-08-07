using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TrackWallet.Services;
using TrakWallet.Data;
using TrakWallet.Models;

public class CustomUserStore :UserStore<AppUser, IdentityRole, AppDbContext, string>
{
    private readonly WalletInitializationService _walletInitializationService;
    public CustomUserStore(AppDbContext context, WalletInitializationService walletInitializationService)
        : base(context)
    {
        _walletInitializationService = walletInitializationService;
    }
    public override async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken = default)
    {
        var result = await base.CreateAsync(user, cancellationToken);
        if (result.Succeeded)
        {

            string fromMail = "abdullahyahyaalshami2001@gmail.com";
            string fromPassword = "xfye hlnc smme incb";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Welcome";
            message.To.Add(new MailAddress(user.Email!));
            message.Body = "<html><body>Hi, welcome to my website</body></html>";
            message.IsBodyHtml = true;

            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential(fromMail, fromPassword);
                smtpClient.EnableSsl = true;

                try
                {
                    smtpClient.Send(message);
                    Console.WriteLine("Email sent successfully!");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
            }
            await _walletInitializationService.InitializeWalletsForUserAsync(user);
        }
        return result;
    }
}
