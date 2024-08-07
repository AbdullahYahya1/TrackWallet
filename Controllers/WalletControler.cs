using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackWallet.DTO;
using TrackWallet.IRepo;
using TrakWallet.Models;

namespace TrackWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepo _walletRepo;
        private readonly UserManager<AppUser> _userManager;

        public WalletController(IWalletRepo walletRepo, UserManager<AppUser> userManager)
        {
            _walletRepo = walletRepo;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddWallet([FromForm] WalletDto walletDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { message = "User not authorized." });
            }

            try
            {
                var wallet = await _walletRepo.AddWalletAsync(user, walletDto);
                return Ok(new { wallet = wallet, status = true });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An unexpected error occurred: {ex.Message}" });
            }
        }

        [HttpGet("wallets")]
        public async Task<IActionResult> GetWallets()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { message = "User not authorized." });
            }

            try
            {
                var wallets = await _walletRepo.GetWalletsAsync(user);
                return Ok(new { wallets = wallets, status = true });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An unexpected error occurred: {ex.Message}" });
            }
        }

        [HttpGet("wallet/{walletId}")]
        public async Task<IActionResult> GetWallet(int walletId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { message = "User not authorized." });
            }

            try
            {
                var wallet = await _walletRepo.GetWalletAsync(user, walletId);
                return Ok(new { wallet = wallet, status = true });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An unexpected error occurred: {ex.Message}" });
            }
        }

        [HttpDelete("wallet/{walletId}")]
        public async Task<IActionResult> RemoveWallet(int walletId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { message = "User not authorized." });
            }

            try
            {
                var result = await _walletRepo.RemoveTask(user, walletId);
                return Ok(new { status = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An unexpected error occurred: {ex.Message}" });
            }
        }

        [HttpPut("wallet/{walletId}")]
        public async Task<IActionResult> UpdateWallet(int walletId, [FromForm] WalletDto walletDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { message = "User not authorized." });
            }

            try
            {
                var updatedWallet = await _walletRepo.UpdateWalletAsync(user, walletId, walletDto);
                return Ok(new { wallet = updatedWallet, status = true });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An unexpected error occurred: {ex.Message}" });
            }
        }
    }
}
