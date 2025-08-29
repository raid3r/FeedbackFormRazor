using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FeedbackFormRazor.Models.Services.Feedback;

public class StoreFeedbackToJsonArray : IFeedbackSender
{
    public async Task SendFeedbackAsync(FeedbackForm feedbackForm)
    {
        var uploadsDir = Path.Combine("wwwroot", "uploads");

        var storageFileDir = Path.Combine("storage");
        if (!Directory.Exists(storageFileDir))
        {
            Directory.CreateDirectory(storageFileDir);
        }
        var storageFilePath = Path.Combine(storageFileDir, "feedbacks.json");

        // get feedbacks array from file 
        if (!File.Exists(storageFilePath))
        {
            await File.WriteAllTextAsync(storageFilePath, "[]");
        }
        using var reader = new StreamReader(storageFilePath);
        var fileContent = await reader.ReadToEndAsync();
        var feedbacks = string.IsNullOrEmpty(fileContent)
            ? new List<FeedbackModel>()
            : System.Text.Json.JsonSerializer.Deserialize<List<FeedbackModel>>(fileContent);
        reader.Close();
        reader.Dispose();

        var imagePath = string.Empty;
        var nextId = feedbacks.Any() ? feedbacks.Max(f => f.Id) + 1 : 1;
        if (feedbackForm.Image != null)
        {
            var imageExt = Path.GetExtension(feedbackForm.Image.FileName).ToLower();
            var newImageName = $"image_{DateTime.Now:yyyyMMdd_HHmmss}{imageExt}";
            imagePath = Path.Combine(uploadsDir, newImageName);
            using var fileStream = System.IO.File.Create(imagePath);
            await feedbackForm.Image.CopyToAsync(fileStream);
        }

        feedbacks.Add(new FeedbackModel
        {
            Id = nextId,
            Name = feedbackForm.Name,
            Gender = feedbackForm.Gender,
            Birthday = feedbackForm.Birthday,
            Comment = feedbackForm.Comment,
            Country = feedbackForm.Country,
            Email = feedbackForm.Email,
            Favorites = feedbackForm.Favorites,
            ImageFile = imagePath,
        });

        var newFileContent = JsonSerializer.Serialize(feedbacks,
            new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });
        await File.WriteAllTextAsync(storageFilePath, newFileContent);
    }
}
