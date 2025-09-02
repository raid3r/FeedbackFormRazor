using FeedbackFormRazor.Models;
using FeedbackFormRazor.Models.Services.Feedback;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FeedbackFormRazor.Pages.Feedback;

public class FeedbackPageModel(IFeedbackSender feedbackSender) : PageModel
{
    [BindProperty]
    public FeedbackForm FeedbackForm { get; set; } = new();

    public void OnGet()
    {
        var a = 5;

        var b = a + a;
        Console.WriteLine("Test");

    }

    // public void OnPost()
    // public IActionResult OnPost()
    // public async Task<IActionResult> OnPost()


    public async Task<IActionResult> OnPost()
    {
        // �������� �����
        if (!string.IsNullOrEmpty(FeedbackForm.Name) && FeedbackForm.Name.Length < 3)
        {
            ModelState.AddModelError("FeedbackForm.Name", "3 �� ����� �������");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        // ������� ����� �����
        // Process the feedback form data (e.g., save to database, send email, etc.)
        await feedbackSender.SendFeedbackAsync(FeedbackForm);

        // ������������� �� ������� ������
        return RedirectToPage("/Feedback/ThankYou");
    }
}

/*
 * ������� ����� ���������� ��'���� � ������������� Razor Pages � ASP.NET Core
 * ���� �� ���� �����, �� �������� ��� ����� � �������� ���� �� ������
 * ϳ��� �������� ����� ���������� ������� � ������� 
 * 
 * //if (FeedbackForm.Image != null)
        //{
        //    // ���������� ����� ���� ��������� �� �볺���

        //    var file = FeedbackForm.Image;

        //    using var fileStream = System.IO.File.Create($"wwwroot/uploads/{file.FileName}");
        //    file.CopyTo(fileStream);
        //}

 * 
 */ 