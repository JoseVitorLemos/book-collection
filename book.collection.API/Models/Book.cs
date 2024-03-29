using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Models
{
  [Table("books")]
  public class Book : BaseEntity
  {
    [Required]
    [MaxLength(80)]
    public string name { get; set; }

    [Required]
    [MaxLength(80)]
    public string edition { get; set; }

    [Required]
    [MaxLength(80)]
    public DateTime release_date { get; set; }
    
    [Required]
    [Column("publishing_companies_id")]
    public Guid publishingCompanieId { get; set; }

    public ICollection<BookAuthor> BookAuthors { get; set; }
  }
}
