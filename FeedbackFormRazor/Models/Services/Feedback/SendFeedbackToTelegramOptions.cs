namespace FeedbackFormRazor.Models.Services.Feedback;

public class SendFeedbackToTelegramOptions
{
    public string BotToken { get; set; }
    public long ChatId { get; set; }
}
