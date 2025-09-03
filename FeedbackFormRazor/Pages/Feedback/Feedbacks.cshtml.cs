using FeedbackFormRazor.Models;
using FeedbackFormRazor.Models.Services.Feedback;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackFormRazor.Pages.Feedback;

public class FeedbacksModel(FeedbackRepository feedbackRepository) : PageModel
{
    public List<FeedbackModel> Feedbacks { get; set; } = [];

    /// <summary>
    /// ���� ��� ���������� Name | Email | Gender | null
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public string SortField { get; set; }

    /// <summary>
    /// ����� ���������� asc | desc
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public string SortOrder { get; set; }

    /// <summary>
    /// ����� �������
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public int PageSize { get; set; } = 5;

    /// <summary>
    /// ����� �������
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// �������� ������� ������
    /// </summary>
    public int TotalCount { get; set; } = 0;

    /// <summary>
    /// �������� ������� �������
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);

    /// <summary>
    /// ����� ���������� ��� GET ����� �� �������
    /// </summary>
    /// <returns></returns>
    public async Task OnGet()
    {
        Feedbacks = await feedbackRepository.GetAllAsync();
        TotalCount = Feedbacks.Count;

        // ����������
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

        // ��������
        Feedbacks = Feedbacks
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize)
            .ToList();
    }

    /// <summary>
    /// �������� ����� �� ���������������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnPostDelete(int id)
    {
        await feedbackRepository.DeleteAsync(id);
        return RedirectToPage();
    }
}
