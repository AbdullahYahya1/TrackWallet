using TrakWallet.Data;
using TrakWallet.Models;

namespace TrackWallet.Services
{
    public class WalletInitializationService
    {
        private readonly AppDbContext _context;
        public WalletInitializationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task InitializeWalletsForUserAsync(AppUser user)
        {
            var defaultWallets = new List<Wallet>
        {
            new Wallet { Name = "Bank", Balance = 0, UserId = user.Id, CreatedDate = DateTime.UtcNow },
            new Wallet { Name = "Personal wallet", Balance = 0, UserId = user.Id, CreatedDate = DateTime.UtcNow },
            new Wallet { Name = "Savings wallet", Balance = 0, UserId = user.Id, CreatedDate = DateTime.UtcNow },
            new Wallet { Name = "Expense wallet", Balance = 0, UserId = user.Id, CreatedDate = DateTime.UtcNow },
            new Wallet { Name = "Travel wallet", Balance = 0, UserId = user.Id, CreatedDate = DateTime.UtcNow },
            new Wallet { Name = "Emergency Fund wallet", Balance = 0, UserId = user.Id, CreatedDate = DateTime.UtcNow }
        };
            _context.Wallets.AddRange(defaultWallets);
            await _context.SaveChangesAsync();
        }
    }
}
