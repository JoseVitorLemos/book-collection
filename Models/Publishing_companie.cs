using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Models
{
  [Table("publishing_companies")]
  public class PublishingCompanie
  {
    [Key]
    public int id { get; set; }

    [Required]
    [MaxLength(80)]
    public string cnpj { get; set; }

    [Required]
    [MaxLength(80)]
    public string name { get; set; }

    [Required]
    [MaxLength(80)]
    public string adress { get; set; }

    [MaxLength(80)]
    public string cep { get; set; }

    [Required]
    public int profile_id { get; set; }
  }
}
