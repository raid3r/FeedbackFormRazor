using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FeedbackFormRazor.Models.Services.Feedback;

public class StoreFeedbackToJsonArray(
    FeedbackRepository feedbackRepository,
    UploadedFileStorage uplodedFileStorage
    ) : IFeedbackSender
{
    public async Task SendFeedbackAsync(FeedbackForm feedbackForm)
    {
        // Збереження файлу який переданий від клієнта
        var imagePath = string.Empty;
        if (feedbackForm.Image != null)
        {
            imagePath = await uplodedFileStorage.SaveFileAsync(feedbackForm.Image);
        }

        // Збереження даних форми
        await feedbackRepository.AddAsync(new FeedbackModel
        {
            Name = feedbackForm.Name,
            Gender = feedbackForm.Gender,
            Birthday = feedbackForm.Birthday,
            Comment = feedbackForm.Comment,
            Country = feedbackForm.Country,
            Email = feedbackForm.Email,
            Favorites = feedbackForm.Favorites,
            ImageFile = imagePath,
        });
    }
}
