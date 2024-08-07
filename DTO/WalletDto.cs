using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.DTO
{
    public class WalletDto
    {
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
    }
}
