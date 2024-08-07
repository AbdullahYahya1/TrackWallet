using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TrackWallet.Models;

namespace TrakWallet.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Wallet> Wallets { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<RecurringTransaction> RecurringTransactions { get; set; }
    }
}
