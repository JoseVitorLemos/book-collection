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
    public PublishingCompanie()
    {
      Books = new Collection<Book>();
      Authors = new Collection<Author>();
    }

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
    [Column("profile_id")]
    public int profilesId { get; set; }

    public ICollection<Book> Books { get; set; }

    public ICollection<Author> Authors { get; set; }
  }
}
