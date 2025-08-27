using System.Net.Mail;

namespace FeedbackFormRazor.Models.Services.Feedback;

public class SendFeedbackToEmail : IFeedbackSender
{
    public async Task SendFeedbackAsync(FeedbackForm feedbackForm)
    {
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

        var from = "";
        var to = "";
        var password = ""; // Use app password

        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(from);
            mail.To.Add(to);
            mail.Subject = "New Feedback Received";
            mail.Body = htmlContent;
            mail.IsBodyHtml = true;

            if (feedbackForm.Image != null)
            {
                var attachment = new Attachment(feedbackForm.Image.OpenReadStream(), feedbackForm.Image.FileName);
                mail.Attachments.Add(attachment);
            }

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new System.Net.NetworkCredential(from, password); // Use app password
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }

        }
    }
}
