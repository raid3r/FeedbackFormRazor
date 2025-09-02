using FeedbackFormRazor.Models;
using FeedbackFormRazor.Models.Services.Feedback;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackFormRazor.Pages.Feedback;

public class FeedbacksModel(FeedbackRepository feedbackRepository) : PageModel
{
    public List<FeedbackModel> Feedbacks { get; set; } = [];


    public async Task OnGet()
    {
        Feedbacks = await feedbackRepository.GetAllAsync();

    }
    // form method="post asp-page-handler="Delete"
    public async Task<IActionResult> OnPostDelete(int id)
    {
        await feedbackRepository.DeleteAsync(id);
        return RedirectToPage();
    }
}
