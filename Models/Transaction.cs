using System;
using System.ComponentModel.DataAnnotations;
using TrakWallet.Models;

namespace TrackWallet.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        [RegularExpression("^(income|expense)$", ErrorMessage = "Type must be either 'income' or 'expense'.")]
        public string Type { get; set; } // 'income' or 'expense'
        public string? Url { get; set; }

        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
