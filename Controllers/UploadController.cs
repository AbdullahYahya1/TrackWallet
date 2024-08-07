using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using TrackWallet.Services;

namespace TrackWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ImageUploadService _imageUploadService;

        public UploadController(IWebHostEnvironment env, ImageUploadService imageUploadService)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _imageUploadService = imageUploadService ?? throw new ArgumentNullException(nameof(imageUploadService));
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "No file provided or file is empty." });

            try
            {
                var url = await _imageUploadService.UploadImageAsync(file, Request);
                return Ok(new { url = url });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An unexpected error occurred: {ex.Message}" });
            }
        }

        [HttpDelete("delete-image")]
        public async Task<IActionResult> DeleteImage(string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest(new { message = "Image URL is required." });

            try
            {
                var result = await _imageUploadService.DeleteImageAsync(url);
                if (result)
                    return Ok(new { message = "Image deleted successfully." });
                else
                    return NotFound(new { message = "Image not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An unexpected error occurred: {ex.Message}" });
            }
        }
    }
}
