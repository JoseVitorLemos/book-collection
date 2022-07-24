using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Dto
{
  public class ResponseProfileDto
  {
    public string cpf { get; set; }

    public string name { get; set; }

    public DateTime birth_date { get; set; }

    public string email { get; set; }
  }
}
