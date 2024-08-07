using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackWallet.DTO;
using TrackWallet.IRepo;
using TrakWallet.Data;
using TrakWallet.Models;

namespace TrackWallet.Repo
{
    public class WalletRepo : IWalletRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public WalletRepo(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetWalletDto> AddWalletAsync(AppUser user, WalletDto walletDto)
        {
            if (user == null)
                throw new UnauthorizedAccessException("User is not authorized");
            var wallet = _mapper.Map<Wallet>(walletDto);
            wallet.UserId = user.Id;
            wallet.CreatedDate = DateTime.UtcNow; // Set the CreatedDate to current date and time
            _context.Add(wallet);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetWalletDto>(wallet);
        }

        public async Task<GetWalletDto> GetWalletAsync(AppUser user, int WalletID)
        {
            if (user == null)
                throw new UnauthorizedAccessException("User is not authorized");

            var wallet = await _context.Wallets.Include(w => w.Transactions).FirstOrDefaultAsync(w => w.Id == WalletID);
            if (wallet == null)
                throw new KeyNotFoundException("No wallet found for the specified user and category");
            if (user.Id != wallet.UserId)
                throw new UnauthorizedAccessException("User is not authorized for this wallet");

            return _mapper.Map<GetWalletDto>(wallet);
        }

        public async Task<List<GetWalletsDto>> GetWalletsAsync(AppUser user)
        {
            if (user == null)
                throw new UnauthorizedAccessException("User is not authorized");

            var wallets = await _context.Wallets.Where(w => w.UserId == user.Id).ToListAsync();
            return _mapper.Map<List<GetWalletsDto>>(wallets);
        }

        public async Task<bool> RemoveTask(AppUser user, int WalletID)
        {
            if (user == null)
                throw new UnauthorizedAccessException("User is not authorized");

            var wallet = await _context.Wallets.FindAsync(WalletID);
            if (wallet == null)
                throw new KeyNotFoundException("No wallet found with the specified ID");
            if (user.Id != wallet.UserId)
                throw new UnauthorizedAccessException("User is not authorized for this wallet");

            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GetWalletDto> UpdateWalletAsync(AppUser user, int WalletID, WalletDto walletDto)
        {
            if (user == null)
                throw new UnauthorizedAccessException("User is not authorized");

            var wallet = await _context.Wallets.FindAsync(WalletID);
            if (wallet == null)
                throw new KeyNotFoundException("No wallet found with the specified ID");
            if (user.Id != wallet.UserId)
                throw new UnauthorizedAccessException("User is not authorized for this wallet");
            _mapper.Map(walletDto, wallet);
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetWalletDto>(wallet);
        }
    }
}
