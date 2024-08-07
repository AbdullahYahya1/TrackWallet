using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TrackWallet.Models;
using TrakWallet.Models;

public class Wallet
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public DateTime CreatedDate { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<RecurringTransaction> RecurringTransactions { get; set; }
}
