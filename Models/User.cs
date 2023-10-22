using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Desafio_Balta.Models
{
    public class User
    {
        [Key]
        [DisplayName("Id")]
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }
    }
}
