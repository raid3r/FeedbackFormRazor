using FeedbackFormRazor.Models;
using FeedbackFormRazor.Models.Services.Feedback;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackFormRazor.Pages.Feedback;

public class FeedbacksModel(FeedbackRepository feedbackRepository) : PageModel
{
    public List<FeedbackModel> Feedbacks { get; set; } = [];

    /// <summary>
    /// Поле для сортування Name | Email | Gender | null
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public string SortField { get; set; }

    /// <summary>
    /// Спосіб сортування asc | desc
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public string SortOrder { get; set; }

    /// <summary>
    /// Розмір сторінки
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public int PageSize { get; set; } = 5;

    /// <summary>
    /// Номер сторінки
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Загальна кількість відгуків
    /// </summary>
    public int TotalCount { get; set; } = 0;

    /// <summary>
    /// Загальна кількість сторінок
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);

    /// <summary>
    /// Метод виконується при GET запиті до сторінки
    /// </summary>
    /// <returns></returns>
    public async Task OnGet()
    {
        Feedbacks = await feedbackRepository.GetAllAsync();
        TotalCount = Feedbacks.Count;

        // Сортування
        switch (SortField)
        {
            case nameof(FeedbackModel.Name):
                Feedbacks =
                    SortOrder == "asc" ?
                    Feedbacks.OrderBy(x => x.Name).ToList() :
                    Feedbacks.OrderByDescending(x => x.Name).ToList();
                break;
            case nameof(FeedbackModel.Email):
                Feedbacks =
                    SortOrder == "asc" ?
                    Feedbacks.OrderBy(x => x.Email).ToList() :
                    Feedbacks.OrderByDescending(x => x.Email).ToList();
                break;
            case nameof(FeedbackModel.Gender):
                Feedbacks =
                    SortOrder == "asc" ?
                    Feedbacks.OrderBy(x => x.Gender).ToList() :
                    Feedbacks.OrderByDescending(x => x.Gender).ToList();
                break;
            default:
                break;
        }

        // Пагінація
        Feedbacks = Feedbacks
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize)
            .ToList();
    }

    /// <summary>
    /// Видалити відгук за ідентифікатором
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnPostDelete(int id)
    {
        await feedbackRepository.DeleteAsync(id);
        return RedirectToPage();
    }
}
