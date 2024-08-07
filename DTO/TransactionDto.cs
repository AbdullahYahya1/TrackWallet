using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace TrackWallet.DTO
{
    public class TransactionDto
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        [RegularExpression("^(income|expense)$", ErrorMessage = "Type must be either 'income' or 'expense'.")]
        public string Type { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int WalletId { get; set; }
    }
}