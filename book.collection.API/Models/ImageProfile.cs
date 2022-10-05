using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Models
{
  [Table("images_profile")]
  public class ImageProfile : BaseEntity
  {
    [Required]
    [MaxLength(80)]
    public string title { get; set; }

    [Required]
    [Column("profile_id")]
    public Guid profilesId { get; set; }

    [Required]
    public byte[] image_byte { get; set; }
  }
}
