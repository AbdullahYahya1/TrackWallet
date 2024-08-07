using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Utilities;
using TrackWallet.DTO;
using TrackWallet.IRepo;
using TrakWallet.Models;

namespace TrackWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoryController : ControllerBase
    {
        private readonly ICatagoryRepo _CatagoryRepo;
        private readonly UserManager<AppUser> _userManager;

        public CatagoryController(ICatagoryRepo CatagoryRepo, UserManager<AppUser> userManager)
        {
            _CatagoryRepo = CatagoryRepo;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> getcatagorys()
        {
            var c =await _CatagoryRepo.GetCatagorysAsync();
            return Ok(c);
        }
        [HttpGet("SumCatagorys/{lastD}")]
        public async Task<IActionResult> getSc(int lastD) {
            var user =  await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return Unauthorized();
            }
            var c = await _CatagoryRepo.GetSumC(user.Id,-lastD);
            return Ok(
                new {
                    income= c[0],
                    expense = c[1]
                }
            ); 
        }
    }
}
