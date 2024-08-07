using AutoMapper;
using TrackWallet.DTO;
using TrackWallet.Models;

namespace TrackWallet.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Transaction, GetTransactionDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category)).ReverseMap();
            CreateMap<Category, catagorydto>().ReverseMap();
            CreateMap<Category, GetCatagoryDto>().ReverseMap();


            CreateMap<Models.Transaction, TransactionDto>().ReverseMap();
            CreateMap<Models.Transaction, GetTransactionDto>().ReverseMap();
            CreateMap<Wallet, GetWalletDto>()
            .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions)).ReverseMap();
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<Wallet, GetWalletsDto>().ReverseMap();


        }
    }
}
