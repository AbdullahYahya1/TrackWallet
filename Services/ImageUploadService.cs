public class ImageUploadService
{
    private readonly IWebHostEnvironment _env;

    public ImageUploadService(IWebHostEnvironment env)
    {
        _env = env ?? throw new ArgumentNullException(nameof(env));
    }

    public async Task<string> UploadImageAsync(IFormFile file, HttpRequest request)
    {
        if (file == null || file.Length == 0)
            return null;

        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        var relativePath = Path.Combine("uploads", uniqueFileName).Replace("\\", "/");
        var url = $"{request.Scheme}://{request.Host}{request.PathBase}/{relativePath}";
        return url;
    }
    public async Task<bool> DeleteImageAsync(string imageUrl)
    { 
        if (string.IsNullOrEmpty(imageUrl))
            return false;

        var uri = new Uri(imageUrl);
        var filePath = Path.Combine(_env.WebRootPath, uri.LocalPath.TrimStart('/'));

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }

        return false;
    }
}
