using FeedbackFormRazor.Models.Services.Feedback;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddTransient<IFeedbackSender, StoreFeedbackToFile>();
//builder.Services.AddTransient<IFeedbackSender, SendFeedbackToTelegram>();
//builder.Services.AddTransient<IFeedbackSender, SendFeedbackToEmail>();
builder.Services.AddTransient<IFeedbackSender, StoreFeedbackToJsonArray>();


builder.Services.Configure<SendFeedbackToEmailOptions>(
    builder.Configuration.GetSection("SendFeedbackToEmailOptions")
    );
builder.Services.Configure<SendFeedbackToTelegramOptions>(
    builder.Configuration.GetSection("SendFeedbackToTelegramOptions")
    );
builder.Services.Configure<StoreFeedbackToFileOptions>(
    builder.Configuration.GetSection("StoreFeedbackToFileOptions")
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

/*
 * Форма зворотного зв'язку з використанням Razor Pages в ASP.NET Core
 *  
 * 
 * Доробити відправку форми на email, Telegram та збереження у файл
 * 
 *
 */

/*
 * 1.
 * Винести змінні параметри у appsettings.json для усіх сервісів відправки вігуків
 * 
 * 
 * 2.
 * Створити сервіс який буде зберігати відгуки у JSON файл
 * 
 * Створити сторінку "Відгуки" де будуть виводитися усі відгуки з JSON файлу
 * На сторінці таблиця з відгуками, біля кожного кнопки "Переглянути" та "Видалити"
 * При натисканні на "Переглянути" відкривається вікно з детальною інформацією по відгуку
 * При натисканні на "Видалити" - відгук видаляється
 * 
 * Створити сторінку "Перегляд відгуку" де буде детальна інформація по відгуку
 * На ньому виводиться інформація по відгуку а також зображення якщо воно було прикріплене
 * Також є кнопка "Повернутися до списку відгуків"
 * 
 */ 