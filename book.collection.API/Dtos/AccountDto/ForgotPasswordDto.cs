using System.ComponentModel.DataAnnotations;

namespace book_collection.Dto
{
  public class ForgotPasswordDto
  {
    [Required(ErrorMessage = "email is required")]
    [MaxLength(300)]
    public string email { get; set; }
  }
}
