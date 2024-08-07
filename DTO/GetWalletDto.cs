using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.DTO
{
    public class GetWalletDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string UserId { get; set; }
        public List<GetTransactionDto> Transactions { get; set; }
        public DateTime CreatedDate { get; set; } 
    }
}
