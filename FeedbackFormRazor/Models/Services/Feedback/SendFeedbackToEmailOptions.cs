namespace FeedbackFormRazor.Models.Services.Feedback;

public class SendFeedbackToEmailOptions
{
    public string EmailFrom { get; set; }
    public string EmailTo { get; set; }
    public string MailServerPassword { get; set; }
    public string MailServerHost { get; set; }
    public int MailServerPort { get; set; }
    public string MailSubject { get; set; }
}
