using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrackWallet.IRepo;
using TrakWallet.Models;

namespace TrackWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet("CheckEmail")]
        public async Task<IActionResult> checkEmail()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            if (user.EmailConfirmed)
            {
                return Ok(new { status = true });
            }
            else
            {
                return Ok(new { status = false });
            }
        }

    }
}
