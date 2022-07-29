using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Dto
{
  public class CreateProfileDto
  {
    [Required(ErrorMessage = "cpf is required")]
    [MaxLength(80)]
    public string cpf { get; set; }

    [Required(ErrorMessage = "name is required")]
    [MaxLength(80)]
    public string name { get; set; }

    [Required(ErrorMessage = "birth date is required")]
    public DateTime birth_date { get; set; }

    [Required(ErrorMessage = "email is required")]
    [MaxLength(300)]
    public string email { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(20)]
    public string password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("password", ErrorMessage = "password and passwordConfirmation are not the same")]
    [NotMapped]
    public string passwordConfirmation { get; set; }
  }
}
