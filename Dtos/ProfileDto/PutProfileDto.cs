using System.ComponentModel.DataAnnotations;

namespace book_collection.Dto
{
  public class PutProfileDto
  {
    public string name { get; set; }

    public DateTime birth_date { get; set; }
  }
}
