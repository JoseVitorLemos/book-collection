using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Models
{
  [Table("profiles")]
  public class Profile
  {
    [Key]
    public int id { get; set; }

    [Required]
    [MaxLength(80)]
    public string cpf { get; set; }

    [Required]
    public byte[] image { get; set; }

    [Required]
    [MaxLength(80)]
    public string name { get; set; }

    [Required]
    [MaxLength(80)]
    public DateTime birth_date { get; set; }

    [Required]
    [MaxLength(300)]
    public string email { get; set; }

    [Required]
    [MaxLength(80)]
    public string password { get; set; }
  }
}
