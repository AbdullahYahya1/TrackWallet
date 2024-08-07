using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TrackWallet.DTO;
using TrackWallet.IRepo;
using TrackWallet.Models;
using TrakWallet.Data;
using TrakWallet.Models;

namespace TrackWallet.Repo
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly AppDbContext _context;
        private readonly ImageUploadService _imageUploadService;
        private readonly IMapper _mapper;

        public TransactionRepo(AppDbContext context, ImageUploadService imageUploadService, IMapper mapper)
        {
            _context = context;
            _imageUploadService = imageUploadService;
            _mapper = mapper;
        }

        public async Task<GetTransactionDto> AddTransactionAsync(AppUser user, TransactionDto transactionDto, HttpRequest request)
        {
            if (user == null)
                throw new UnauthorizedAccessException("User is not authorized");




            string imageUrl = null;
            if (transactionDto.ImageFile != null)
            {
                imageUrl = await _imageUploadService.UploadImageAsync(transactionDto.ImageFile, request);
            }

            var transaction = _mapper.Map<Transaction>(transactionDto);
            transaction.UserId = user.Id;
            transaction.Url = imageUrl;


            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Id == transaction.WalletId);


            if (wallet == null)
                throw new KeyNotFoundException("Wallet not found.");



            if (wallet.UserId != user.Id)
            {
                throw new UnauthorizedAccessException("wallet is not yours to add anything to it");
            }

            if (transaction.Type == "income")
            {
                wallet.Balance += transaction.Amount;
            }
            else if (transaction.Type == "expense")
            {
                wallet.Balance -= transaction.Amount;
            }


            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            var t = _context.Transactions.Include(t => t.Category).FirstOrDefault(t => t.Id == transaction.Id);
            return _mapper.Map<GetTransactionDto>(t);
        }

        public async Task<List<GetTransactionDto>> GetTransactionsAsync(AppUser user, int categoryId)
        {
            if (user == null)
                throw new UnauthorizedAccessException("User is not authorized to do this.");

            if (categoryId <= 0)
                throw new ArgumentOutOfRangeException(nameof(categoryId), "Category ID must be greater than zero.");

            var transactions = await _context.Transactions
                .Where(t => t.UserId == user.Id && t.CategoryId == categoryId)
                .ToListAsync();

            if (transactions == null || transactions.Count == 0)
                throw new KeyNotFoundException("No transactions found for the specified user and category.");

            return _mapper.Map<List<GetTransactionDto>>(transactions);
        }

        public async Task<List<GetTransactionDto>> GetTransactionsAsync(AppUser user)
        {
            if (user == null)
                throw new UnauthorizedAccessException("User is not authorized to do this.");
            var transactions = await _context.Transactions.Include(t => t.Category)
                .Where(t => t.UserId == user.Id)
                .OrderByDescending(t => t.Id)
                .ToListAsync();
            if (transactions == null || transactions.Count == 0)
                throw new KeyNotFoundException("No transactions found for the specified user.");
            return _mapper.Map<List<GetTransactionDto>>(transactions);
        }

        public async Task<bool> RemoveTransaction(AppUser user, int transactionID)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == transactionID);
            if (transaction == null)
                throw new KeyNotFoundException("Transaction not found.");

            if (user.Id != transaction.UserId)
                throw new UnauthorizedAccessException("User is not authorized to do this.");




            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Id == transaction.WalletId);
            if (wallet == null)
                throw new KeyNotFoundException("Wallet not found.");

            if (transaction.Type == "income")
            {
                wallet.Balance -= transaction.Amount;
            }
            else if (transaction.Type == "expense")
            {
                wallet.Balance += transaction.Amount;
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GetTransactionDto> UpdateTransactionAsync(AppUser user, int transactionID, TransactionDto transactionDto, HttpRequest request)
        {
            if (user == null)
                throw new UnauthorizedAccessException("User is not authorized to do this.");

            if (transactionID <= 0)
                throw new ArgumentOutOfRangeException(nameof(transactionID), "Transaction ID must be greater than zero.");

            var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == transactionID);
            if (transaction == null)
                throw new KeyNotFoundException("Transaction not found.");

            if (user.Id != transaction.UserId)
                throw new UnauthorizedAccessException("User is not authorized to do this.");

            var oldWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Id == transaction.WalletId);
            if (oldWallet == null)
                throw new KeyNotFoundException("Old wallet not found.");

            if (oldWallet.UserId != user.Id)
                throw new UnauthorizedAccessException("Old wallet is not yours to update.");

            if (transaction.Type == "income")
            {
                oldWallet.Balance -= transaction.Amount;
            }
            else if (transaction.Type == "expense")
            {
                oldWallet.Balance += transaction.Amount;
            }

            if (transactionDto.ImageFile != null)
            {
                string imageUrl = await _imageUploadService.UploadImageAsync(transactionDto.ImageFile, request);
                transaction.Url = imageUrl;
            }

            _mapper.Map(transactionDto, transaction);

            var newWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Id == transaction.WalletId);
            if (newWallet == null)
                throw new KeyNotFoundException("New wallet not found.");

            if (newWallet.UserId != user.Id)
                throw new UnauthorizedAccessException("New wallet is not yours to update.");

            if (transaction.Type == "income")
            {
                newWallet.Balance += transaction.Amount;
            }
            else if (transaction.Type == "expense")
            {
                newWallet.Balance -= transaction.Amount;
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<GetTransactionDto>(transaction);
        }


    }
}