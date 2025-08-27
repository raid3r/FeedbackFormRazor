
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FeedbackFormRazor.Models.Services.Feedback;

public class SendFeedbackToTelegram : IFeedbackSender
{
    public async Task SendFeedbackAsync(FeedbackForm feedbackForm)
    {
        try
        {
            var botToken = "";
            long chatId = 0;


            var bot = new Telegram.Bot.TelegramBotClient(botToken);


            var htmlContent = $@"
        <b>New Feedback Received</b>

        <b>Date:</b> {DateTime.Now:dd.MM.yyyy HH:mm:ss}
        <b>Name:</b> {feedbackForm.Name}
        <b>Email:</b> {feedbackForm.Email}        
        <b>Gender:</b> {feedbackForm.Gender}
        <b>Comment:</b> {feedbackForm.Comment}
        <b>Country:</b> {feedbackForm.Country}
        <b>Birthday:</b> {feedbackForm.Birthday?.ToString("yyyy-MM-dd")}
        <b>Favorites:</b>
        {string.Join("\n", feedbackForm.Favorites.Select(fav => $" - {fav}"))}
";

            await bot.SendMessage(
                chatId: chatId, 
                text: htmlContent, 
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                );

            if (feedbackForm.Image != null)
            {
                using var stream = feedbackForm.Image.OpenReadStream();

                var photo = InputFile.FromStream(stream, feedbackForm.Image.FileName);

                await bot.SendPhoto(
                    chatId: chatId,
                    photo: photo,
                    caption: "Attached Image"
                    );
            }


        }
        catch (Exception ex)
        {

        }



        //var uploadsDir = Path.Combine("wwwroot", "uploads");
        //if (!Directory.Exists(uploadsDir))
        //{
        //    Directory.CreateDirectory(uploadsDir);
        //}

        //var filename = Path.Combine("wwwroot", "uploads", $"feedback_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        //var sb = new System.Text.StringBuilder();
        //sb.AppendLine($"Date: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
        //sb.AppendLine($"Name: {feedbackForm.Name}");
        //sb.AppendLine($"Gender: {feedbackForm.Gender}");
        //sb.AppendLine($"Email: {feedbackForm.Email}");
        //sb.AppendLine($"Comment: {feedbackForm.Comment}");
        //sb.AppendLine($"Country: {feedbackForm.Country}");
        //sb.AppendLine($"Birthday: {feedbackForm.Birthday:yyyy-MM-dd}");
        //sb.AppendLine("Favorites:");
        //foreach (var fav in feedbackForm.Favorites)
        //{
        //    sb.AppendLine($" - {fav}");
        //}

        //if (feedbackForm.Image != null)
        //{
        //    sb.AppendLine($"Image: {feedbackForm.Image.FileName} ({feedbackForm.Image.Length} bytes)");
        //    var imageExt = Path.GetExtension(feedbackForm.Image.FileName).ToLower();
        //    var newImageName = $"image_{DateTime.Now:yyyyMMdd_HHmmss}{imageExt}";
        //    var imagePath = Path.Combine("wwwroot", "uploads", newImageName);
        //    using var fileStream = System.IO.File.Create(imagePath);
        //    await feedbackForm.Image.CopyToAsync(fileStream);
        //}

        //await System.IO.File.WriteAllTextAsync(filename, sb.ToString());
    }
}
