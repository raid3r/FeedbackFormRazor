using FeedbackFormRazor.Models;
using FeedbackFormRazor.Models.Services.Feedback;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackFormRazor.Pages.Feedback;

public class FeedbackDetailsModel(FeedbackRepository feedbackRepository) : PageModel
{
    public FeedbackModel? Feedback { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        Feedback = await feedbackRepository.GetById(id);
        if (Feedback == null)
        {
            //return NotFound();

            return RedirectToPage("/Feedback/Feedbacks");
        }
        return Page();
    }
}
