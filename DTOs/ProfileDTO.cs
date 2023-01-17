using System.ComponentModel.DataAnnotations;

namespace LettercaixaAPI.DTOs
{
    public class ProfileDTO
    {
        [Required (ErrorMessage = "Field is required!")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Field is required!")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Field is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Field is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.ImageUrl)]
        public string? ProfilePicture { get; set; }

        [Required(ErrorMessage = "Field is required!")]
        [DataType(DataType.Date)]
        public DateTime Birth { get; set; }
    }
}
