using TrackWallet.DTO;

namespace TrackWallet.IRepo
{
    public interface ICatagoryRepo
    {
        Task<List<GetCatagoryDto>> GetCatagorysAsync();
        Task<List<List<sumCDto>>> GetSumC(string userId,int last);
    }
}
