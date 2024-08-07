using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Diagnostics;
using TrackWallet.DTO;
using TrackWallet.IRepo;
using TrakWallet.Data;
using TrakWallet.Models;

namespace TrackWallet.Repo
{
    public class CatagoryRepo: ICatagoryRepo
    {
        private readonly AppDbContext _context;
        private readonly ImageUploadService _imageUploadService;
        private readonly IMapper _mapper;

        public CatagoryRepo(AppDbContext context, ImageUploadService imageUploadService, IMapper mapper)
        {
            _context = context;
            _imageUploadService = imageUploadService;
            _mapper = mapper;
        }
        public async Task<List<GetCatagoryDto>> GetCatagorysAsync()
        {
            var c = await _context.Categories.ToListAsync();
            return _mapper.Map<List<GetCatagoryDto>>(c);
        }
        public async Task<List<List<sumCDto>>> GetSumC(string userId , int last)
        {
            var lastd = DateTime.UtcNow.AddDays(last);

            var sumByCategory = await _context.Categories
                .Include(c => c.Transactions)
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TotalAmount = c.Transactions
                        .Where(t => t.UserId == userId && t.Type == "income" && t.Amount != 0 && t.Date >= lastd)
                        .Sum(t => t.Amount)
                })
                .ToListAsync();

            var result = sumByCategory.Select(c => new sumCDto
            {
                Name = c.CategoryName,
                Sum = c.TotalAmount
            }).Where(s=>s.Sum!=0).ToList();


            var sumByCategory2 = await _context.Categories
            .Include(c => c.Transactions)
            .Select(c => new
            {
                CategoryName = c.Name,
                TotalAmount = c.Transactions
                    .Where(t => t.UserId == userId && t.Type == "expense" && t.Amount!= 0 && t.Date >= lastd)
                    .Sum(t => t.Amount)
            })
            .ToListAsync();
            var result2 = sumByCategory2.Select(c => new sumCDto
            {
                Name = c.CategoryName,
                Sum = c.TotalAmount
            }).Where(s => s.Sum != 0).ToList();
            return [result , result2];
        }
    }
}
