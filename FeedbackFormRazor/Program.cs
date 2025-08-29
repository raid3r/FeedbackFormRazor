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
 * ����� ���������� ��'���� � ������������� Razor Pages � ASP.NET Core
 *  
 * 
 * �������� �������� ����� �� email, Telegram �� ���������� � ����
 * 
 *
 */

/*
 * 1.
 * ������� ���� ��������� � appsettings.json ��� ��� ������ �������� �����
 * 
 * 
 * 2.
 * �������� ����� ���� ���� �������� ������ � JSON ����
 * 
 * �������� ������� "³�����" �� ������ ���������� �� ������ � JSON �����
 * �� ������� ������� � ��������, ��� ������� ������ "�����������" �� "��������"
 * ��� ��������� �� "�����������" ����������� ���� � ��������� ����������� �� ������
 * ��� ��������� �� "��������" - ����� �����������
 * 
 * �������� ������� "�������� ������" �� ���� �������� ���������� �� ������
 * �� ����� ���������� ���������� �� ������ � ����� ���������� ���� ���� ���� ����������
 * ����� � ������ "����������� �� ������ ������"
 * 
 */ 