namespace FeedbackFormRazor.Models.Services.Feedback;

public interface IFeedbackSender
{
    public Task SendFeedbackAsync(FeedbackForm feedbackForm);
}
