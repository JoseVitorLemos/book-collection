using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Dto
{
  public class ProfileDto
  {
    [Required]
    [MaxLength(80)]
    public string cpf { get; set; }

    [Required]
    [MaxLength(80)]
    public string name { get; set; }

    [Required]
    public DateTime birth_date { get; set; }

    [Required]
    [MaxLength(300)]
    public string email { get; set; }

    [Required]
    [MaxLength(80)]
    public string password { get; set; }

    [Required]
    [MaxLength(80)]
    public string passwordConfirmation { get; set; }
  }
}
