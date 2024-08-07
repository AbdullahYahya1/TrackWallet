using TrackWallet.DTO;
using TrakWallet.Models;

namespace TrackWallet.IRepo
{
    public interface IWalletRepo
    {
        Task<GetWalletDto> AddWalletAsync(AppUser user, WalletDto walletDto);
        Task<List<GetWalletsDto>> GetWalletsAsync(AppUser user);
        Task<GetWalletDto> GetWalletAsync(AppUser user, int WalletID);
        Task<GetWalletDto> UpdateWalletAsync(AppUser user, int WalletID, WalletDto walletDto);
        Task<bool> RemoveTask(AppUser user, int WalletID);
    }
}
