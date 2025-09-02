namespace FeedbackFormRazor.Models.Services;

public class UploadedFileStorage
{
    public async Task<string> SaveFileAsync(IFormFile file)
    {
        var uploadPath = Path.Combine("wwwroot", "uploads");

        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is null or empty", nameof(file));
        }
        // Ensure the upload directory exists
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
        // Generate a unique filename to avoid overwriting existing files
        var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        var filePath = Path.Combine(uploadPath, uniqueFileName);
        // Save the file to the specified path
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return filePath; // uniqueFileName; // Return the unique filename for reference
    }

    public void DeleteFile(string fileName)
    {
        var uploadPath = Path.Combine("wwwroot", "uploads");
        var filePath = Path.Combine(uploadPath, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

}
