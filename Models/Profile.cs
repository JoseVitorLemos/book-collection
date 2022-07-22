using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Models
{
  [Table("profiles")]
  public class Profile
  {
    public Profile()
    {
      PublishingCompanies = new Collection<PublishingCompanie>();
      ImageProfiles = new Collection<ImageProfile>();
    }

    [Key]
    public int id { get; set; }

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

    public ICollection<PublishingCompanie> PublishingCompanies { get; set; }

    public ICollection<ImageProfile> ImageProfiles { get; set; }
  }
}
