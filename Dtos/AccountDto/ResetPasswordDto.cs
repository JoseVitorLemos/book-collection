using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Dto
{
  public class ResetPasswordDto
  {
    [Required(ErrorMessage = "email is required")]
    [MaxLength(300)]
    [DataType(DataType.EmailAddress)]
    public string email { get; set; }

    [Required(ErrorMessage = "password is required")]
    [MinLength(8)]
    [MaxLength(20)]
    public string password { get; set; }
    
    [MinLength(8)]
    [MaxLength(20)]
    [DataType(DataType.Password)]
    [Compare("password", ErrorMessage = "password and passwordConfirmation are not the same")]
    public string passwordConfirmation { get; set; }

    [MinLength(8)]
    [MaxLength(20)]
    [Required(ErrorMessage = "oldPassword is required")]
    public string oldPassword { get; set; }
  }
}
