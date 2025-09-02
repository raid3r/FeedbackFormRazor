using System.Text.Json;

namespace FeedbackFormRazor.Models.Services.Feedback;

public class FeedbackRepository(UploadedFileStorage uploadedFileStorage)
{
    /// <summary>
    /// Отримати всі відгуки
    /// </summary>
    /// <returns></returns>
    public async Task<List<FeedbackModel>> GetAllAsync()
    {
        // Отримати вміст файлу
        var fileContent = await LoadFileContentAsync();
        // Десеріалізувати вміст файлу у список відгуків
        return string.IsNullOrEmpty(fileContent)
            ? [] : JsonSerializer.Deserialize<List<FeedbackModel>>(fileContent)!;
    }

    /// <summary>
    /// Отримати відгук за ідентифікатором
    /// </summary>
    /// <param name="id">Ідентифікатор відгуку</param>
    /// <returns></returns>
    public async Task<FeedbackModel?> GetById(int id)
    {
        return await GetAllAsync()
            .ContinueWith(t => t.Result.FirstOrDefault(f => f.Id == id));
    }

    /// <summary>
    /// Додати відгук
    /// </summary>
    /// <param name="feedback"></param>
    /// <returns></returns>
    public async Task AddAsync(FeedbackModel feedback)
    {
        // Отримати всі відгуки
        var feedbacks = await GetAllAsync();
        // Присвоїти новий ідентифікатор
        feedback.Id = GetNextId(feedbacks);
        // Додати відгук до списку
        feedbacks.Add(feedback);
        // Зберегти оновлений список у файл
        await SaveFileContentAsync(JsonSerializer.Serialize(feedbacks,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
    }

    /// <summary>
    /// Видалити відгук за ідентифікатором
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        // Отримати всі відгуки
        var feedbacks = await GetAllAsync();
        // Знайти відгук за ідентифікатором
        var feedbackToDelete = feedbacks.FirstOrDefault(f => f.Id == id);
        // Якщо відгук знайдено, видалити його зі списку
        if (feedbackToDelete != null)
        {
            // Видалити файл зображення, якщо потрібно
            if (!string.IsNullOrEmpty(feedbackToDelete.ImageFile))
            {
                uploadedFileStorage.DeleteFile(Path.GetFileName(feedbackToDelete.ImageFile));
            }
            feedbacks.Remove(feedbackToDelete);

            // Зберегти оновлений список у файл
            await SaveFileContentAsync(JsonSerializer.Serialize(feedbacks,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
        }
    }

    // helpers

    /// <summary>
    /// Отримати наступний ідентифікатор зі списку відгуків
    /// </summary>
    /// <param name="feedbacks"></param>
    /// <returns></returns>
    private static int GetNextId(List<FeedbackModel> feedbacks)
    {
        return feedbacks.Count == 0 ? 1 : feedbacks.Max(f => f.Id) + 1;
    }

    /// <summary>
    /// Завантажити вміст файлу json з відгуками
    /// </summary>
    /// <returns></returns>
    private async Task<string> LoadFileContentAsync()
    {
        var storageFileDir = Path.Combine("storage");
        if (!Directory.Exists(storageFileDir))
        {
            Directory.CreateDirectory(storageFileDir);
        }
        var storageFilePath = Path.Combine(storageFileDir, "feedbacks.json");

        // get feedbacks array from file 
        if (!File.Exists(storageFilePath))
        {
            await SaveFileContentAsync("[]");
        }
        using var reader = new StreamReader(storageFilePath);
        return await reader.ReadToEndAsync();
    }


    /// <summary>
    /// Зберегти вміст файлу json з відгуками
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private async Task SaveFileContentAsync(string content)
    {
        var storageFileDir = Path.Combine("storage");
        if (!Directory.Exists(storageFileDir))
        {
            Directory.CreateDirectory(storageFileDir);
        }
        var storageFilePath = Path.Combine(storageFileDir, "feedbacks.json");
        await File.WriteAllTextAsync(storageFilePath, content);
    }
   
}
