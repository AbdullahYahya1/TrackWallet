using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackWallet.DTO;
using TrackWallet.IRepo;
using TrakWallet.Models;

namespace TrackWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly UserManager<AppUser> _userManager;

        public TransactionController(ITransactionRepo transactionRepo, UserManager<AppUser> userManager)
        {
            _transactionRepo = transactionRepo;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromForm] TransactionDto transactionDto)
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
                var transaction = await _transactionRepo.AddTransactionAsync(user, transactionDto, Request);
                return Ok(new { transaction = transaction, status = true });
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

        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { message = "User not authorized." });
            }

            try
            {
                var transactions = await _transactionRepo.GetTransactionsAsync(user);
                return Ok(new { transactions = transactions, status = true });
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

        [HttpGet("transactions/category/{categoryId}")]
        public async Task<IActionResult> GetTransactionsByCategory(int categoryId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { message = "User not authorized." });
            }

            try
            {
                var transactions = await _transactionRepo.GetTransactionsAsync(user, categoryId);
                return Ok(new { transactions = transactions, status = true });
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

        [HttpDelete("transaction/{transactionId}")]
        public async Task<IActionResult> RemoveTransaction(int transactionId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { message = "User not authorized." });
            }

            try
            {
                var result = await _transactionRepo.RemoveTransaction(user, transactionId);
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

        [HttpPut("transaction/{transactionId}")]
        public async Task<IActionResult> UpdateTransaction(int transactionId, [FromForm] TransactionDto transactionDto)
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
                var updatedTransaction = await _transactionRepo.UpdateTransactionAsync(user, transactionId, transactionDto, Request);
                return Ok(new { transaction = updatedTransaction, status = true });
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
