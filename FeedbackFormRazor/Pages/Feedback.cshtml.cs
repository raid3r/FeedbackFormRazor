using FeedbackFormRazor.Models;
using FeedbackFormRazor.Models.Services.Feedback;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FeedbackFormRazor.Pages;

public class FeedbackModel(IFeedbackSender feedbackSender) : PageModel
{
    [BindProperty]
    public FeedbackForm FeedbackForm { get; set; } = new();

    public void OnGet()
    {
    }

    // public void OnPost()
    // public IActionResult OnPost()
    // public async Task<IActionResult> OnPost()


    public async Task<IActionResult> OnPost()
    {
        // Валідація форми
        if (!string.IsNullOrEmpty(FeedbackForm.Name) && FeedbackForm.Name.Length < 3)
        {
            ModelState.AddModelError("FeedbackForm.Name", "3 та більше символів");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Обробка даних форми
        // Process the feedback form data (e.g., save to database, send email, etc.)
        await feedbackSender.SendFeedbackAsync(FeedbackForm);

        // Переадресація на сторінку подяки
        return RedirectToPage("ThankYou");
    }
}

/*
 * Зробити форму зворотного зв'язку з використанням Razor Pages в ASP.NET Core
 * Якщо усі поля валідні, то зебрігати дані форми в тектовий файл на сервері
 * Після відправки форми показувати сторінку з подякою 
 * 
 * //if (FeedbackForm.Image != null)
        //{
        //    // Збереження файлу який переданий від клієнта

        //    var file = FeedbackForm.Image;

        //    using var fileStream = System.IO.File.Create($"wwwroot/uploads/{file.FileName}");
        //    file.CopyTo(fileStream);
        //}

 * 
 */ 