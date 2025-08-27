
namespace FeedbackFormRazor.Models.Services.Feedback;

public class StoreFeedbackToFile : IFeedbackSender
{
    public async Task SendFeedbackAsync(FeedbackForm feedbackForm)
    {
        var uploadsDir = Path.Combine("wwwroot", "uploads");
        if (!Directory.Exists(uploadsDir))
        {
            Directory.CreateDirectory(uploadsDir);
        }

        var filename = Path.Combine("wwwroot", "uploads", $"feedback_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Date: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
        sb.AppendLine($"Name: {feedbackForm.Name}");
        sb.AppendLine($"Gender: {feedbackForm.Gender}");
        sb.AppendLine($"Email: {feedbackForm.Email}");
        sb.AppendLine($"Comment: {feedbackForm.Comment}");
        sb.AppendLine($"Country: {feedbackForm.Country}");
        sb.AppendLine($"Birthday: {feedbackForm.Birthday:yyyy-MM-dd}");
        sb.AppendLine("Favorites:");
        foreach (var fav in feedbackForm.Favorites)
        {
            sb.AppendLine($" - {fav}");
        }
        
        if (feedbackForm.Image != null)
        {
            sb.AppendLine($"Image: {feedbackForm.Image.FileName} ({feedbackForm.Image.Length} bytes)");
            var imageExt = Path.GetExtension(feedbackForm.Image.FileName).ToLower();
            var newImageName = $"image_{DateTime.Now:yyyyMMdd_HHmmss}{imageExt}";
            var imagePath = Path.Combine("wwwroot", "uploads", newImageName);
            using var fileStream = System.IO.File.Create(imagePath);
            await feedbackForm.Image.CopyToAsync(fileStream);
        }

        await System.IO.File.WriteAllTextAsync(filename, sb.ToString());
    }
}
