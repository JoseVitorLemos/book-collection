using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Models
{
  [Table("reset_passwords")]
  public class ResetPassword : BaseEntity
  {
    [Required]
    public string email { get; set; }

    [Required]
    [Column("profile_id")]
    public Guid profilesId { get; set; }
  }
}
