using System.Transactions;
using TrackWallet.DTO;
using TrakWallet.Models;

namespace TrackWallet.IRepo
{
    public interface ITransactionRepo
    {
        Task<GetTransactionDto> AddTransactionAsync(AppUser user,TransactionDto transactionDto , HttpRequest request);
        Task<List<GetTransactionDto>> GetTransactionsAsync(AppUser user, int catagoryID);
        Task<List<GetTransactionDto>> GetTransactionsAsync(AppUser user);
        Task<GetTransactionDto> UpdateTransactionAsync(AppUser user,int transactionID , TransactionDto transactionDto , HttpRequest request);
        Task<bool> RemoveTransaction(AppUser user, int transactionID);
    }
}