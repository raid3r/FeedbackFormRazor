
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FeedbackFormRazor.Models.Services.Feedback;

public class SendFeedbackToTelegram(IOptions<SendFeedbackToTelegramOptions> options) : IFeedbackSender
{
    public async Task SendFeedbackAsync(FeedbackForm feedbackForm)
    {
        // Змінні параметри
        var botToken = options.Value.BotToken;
        long chatId = options.Value.ChatId;


        try
        {
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

    }
}
