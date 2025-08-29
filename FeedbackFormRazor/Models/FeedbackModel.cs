using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FeedbackFormRazor.Models;

public class FeedbackModel
{
    public int Id { get; set; }

    public string Name { get; set; }


    public string Email { get; set; }


    public string Comment { get; set; }

    public string Country { get; set; }

    public DateTime? Birthday { get; set; }

    public string Gender { get; set; }

    public List<string> Favorites { get; set; } = [];

    public string ImageFile { get; set; }
}
