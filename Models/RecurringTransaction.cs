using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrakWallet.Models;

namespace TrackWallet.Models
{
    public class RecurringTransaction
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }


        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string Frequency { get; set; } // e.g., 'daily', 'weekly', 'monthly'
        public string Type { get; set; }  // 'income' or 'expense'
        public DateTime NextRunDate { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }

}
