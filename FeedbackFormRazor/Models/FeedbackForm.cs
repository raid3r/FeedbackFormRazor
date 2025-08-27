using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FeedbackFormRazor.Models;

public class FeedbackForm
{
    [DisplayName("Ваше ім'я")]
    [Required(ErrorMessage = "Введіть ваше ім'я")]
    //[MinLength(3, ErrorMessage = "Від 3-x символів")]
    public string Name { get; set; }


    [DisplayName("Ваша електронна пошта")]
    [Required(ErrorMessage = "Введіть вашу пошту")]
    [EmailAddress(ErrorMessage = "Введіть валідну пошту")]
    public string Email { get; set; }


    [DisplayName("Ваш відгук")]
    [Required(ErrorMessage = "Введіть ваш відгук")]
    public string Comment { get; set; }

    [DisplayName("З якої ви країни")]
    [Required(ErrorMessage = "Оберіть країну зі списку")]
    public string Country { get; set; }

    [DisplayName("Дата народження")]
    public DateTime? Birthday { get; set; }

    [DisplayName("Оберіть вашу стать")]
    [Required(ErrorMessage = "Оберіть з варіантів")]
    public string Gender { get; set; }

    [DisplayName("Що сподобалося?")]
    public List<string> Favorites { get; set; } = [];

    [DisplayName("Додати зображення")]
    public IFormFile? Image { get; set; }

}
