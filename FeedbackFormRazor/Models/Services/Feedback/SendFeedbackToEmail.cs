using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace FeedbackFormRazor.Models.Services.Feedback;

public class SendFeedbackToEmail(IOptions<SendFeedbackToEmailOptions> options) : IFeedbackSender
{

    public async Task SendFeedbackAsync(FeedbackForm feedbackForm)
    {
        // Змінні параметри
        var from = options.Value.EmailFrom;
        var to = options.Value.EmailTo;
        var password = options.Value.MailServerPassword;
        var mailServerHost = options.Value.MailServerHost;
        var mailServerPort = options.Value.MailServerPort;
        var mailSubject = options.Value.MailSubject;


        // сформувати листа

        var htmlContent = $@"
        <h2>New Feedback Received</h2>
        <p><strong>Date:</strong> {DateTime.Now:dd.MM.yyyy HH:mm:ss}</p>
        <p><strong>Name:</strong> {feedbackForm.Name}</p>
        <p><strong>Email:</strong> {feedbackForm.Email}</p>        
        <p><strong>Gender:</strong> {feedbackForm.Gender}</p>
        <p><strong>Comment:</strong> {feedbackForm.Comment}</p>
        <p><strong>Country:</strong> {feedbackForm.Country}</p>
        <p><strong>Birthday:</strong> {feedbackForm.Birthday?.ToString("yyyy-MM-dd")}</p>
        <div>
            <strong>Favorites:</strong>
            <ul>
                 {string.Join("", feedbackForm.Favorites.Select(fav => $"<li>{fav}</li>"))}
            </ul>
        </div>
";

        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(from);
            mail.To.Add(to);
            mail.Subject = mailSubject;
            mail.Body = htmlContent;
            mail.IsBodyHtml = true;

            if (feedbackForm.Image != null)
            {
                var attachment = new Attachment(feedbackForm.Image.OpenReadStream(), feedbackForm.Image.FileName);
                mail.Attachments.Add(attachment);
            }

            using (SmtpClient smtp = new SmtpClient(mailServerHost, mailServerPort))
            {
                smtp.Credentials = new System.Net.NetworkCredential(from, password); // Use app password
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }

        }
    }
}
