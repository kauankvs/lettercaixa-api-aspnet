using System.ComponentModel.DataAnnotations;

namespace LettercaixaAPI.DTOs
{
    public class ProfileLogin
    {
        [Required(ErrorMessage = "Field is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
