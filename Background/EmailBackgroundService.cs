using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using TrackWallet.Models;
using TrakWallet.Models;

namespace TrackWallet.Background
{
    public class EmailBackgroundService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public EmailBackgroundService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            RecurringJob.AddOrUpdate(
                "send-messages-to-users",
                () => SendEmails(),
                "59 23 * * *"); // Run daily at 11:59 PM, which is just before midnight

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        [DisableConcurrentExecution(60)]
        public async Task SendEmails()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var emailSettings = _configuration.GetSection("EmailSettings");
                var mail = "daslforAufd23@outlook.com";
                var pass = "L123456n";

                var client = new SmtpClient("smtp.office365.com", 587)
                {
                    Credentials = new NetworkCredential(mail, pass),
                    EnableSsl = true,
                };

                var confirmedUsers = userManager.Users.Where(user => user.EmailConfirmed).ToList();

                foreach (var user in confirmedUsers)
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(mail),
                        Subject = "Wallet website",
                        Body = "Do not forget to update your transactions",
                        IsBodyHtml = true,
                    };

                    mailMessage.To.Add(user.Email!);
                       await client.SendMailAsync(mailMessage);
                    await Task.Delay(1000);
                }
            }
        }
    }
}
