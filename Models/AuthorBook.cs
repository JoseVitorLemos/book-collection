using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Models
{
  [Table("books_authors")]
  public class BookAuthor 
  {
    [Key]
    public int id { get; set; }

    [Required]
    [Column("book_id")]
    public int BookId { get; set; }

    public Book book { get; set; }

    [Column("author_id")]
    public int AuthorId { get; set; }

    public Author author { get; set; }
  } 
}
